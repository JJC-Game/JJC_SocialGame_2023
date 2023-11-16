using System.Collections;
using Gs2.Unity.Util;
using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Gs2Inventory.Model;
using Gs2.Unity.Gs2Dictionary.Model;


using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Gs2Experience.Model;
using Google.Protobuf.WellKnownTypes;
using Unity.Properties;
using Gs2.Core.Domain;
using Gs2.Gs2Identifier.Model;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class GS2Manager : MonoBehaviour
{
    const string CLIENT_ID = "GKITg_ndOnKcFmHHmXNi3OjII7vK0tu7lTZAjbrQCTm2KWw_Qg_CMW5HGBkKVwKdgaXE_ueEXZBUI4q1NNpDUQKvQ==";
    const string CLIENT_SECRET = "ezrNGluCeYhTODHwyZLlHvfmAOboPHJW";

    const string ACCOUNT_NAMESPACE = "Player";
    const string ACCOUNT_ANGOU_KEY_ID = "grn:gs2:{region}:{ownerId}:key:account-encryption-key-namespace:key:account-encryption-key";

    const string LOGIN_USERID_KEY = "LoginUserId";
    const string LOGIN_PASSWORD_KEY = "LoginPassword";

    const string FRIEND_NAMESPACE = "PlayerProfile001";

    private Profile profile;
    Gs2Domain gs2Domain;
    GameSession gameSession;

    string user_id;
    string password;
    bool isCompleteLogin;

    bool[] hasCharaFlag;

    private void Awake()
    {
        isCompleteLogin = false;
        hasCharaFlag = new bool[DefineParam.CHARA_MAX_ID+1];
        for (int i = 0; i < DefineParam.CHARA_MAX_ID+1; i++)
        {
            hasCharaFlag[i] = false;
        }
    }

    void Start()
    {
        StartCoroutine(InitializeGS2());
    }
    IEnumerator InitializeGS2()
    {
        // Setup general setting
        profile = new Profile(
            CLIENT_ID,
            CLIENT_SECRET,
            reopener: new Gs2BasicReopener()
        );

        // Create GS2 client
        var initializeFuture = profile.InitializeFuture();
        yield return initializeFuture;
        if (initializeFuture.Error != null)
        {
            throw initializeFuture.Error;
        }
        gs2Domain = initializeFuture.Result;

        StartCoroutine(Login());
    }

    private IEnumerator FinalizeGs2()
    {
        if (profile == null)
            yield break;

        yield return profile.Finalize();
    }

    IEnumerator Login()
    {
        // 機材のローカル領域にログイン情報が存在するか.
        if (PlayerPrefs.HasKey(LOGIN_USERID_KEY) && PlayerPrefs.HasKey(LOGIN_PASSWORD_KEY))
        {
            user_id = PlayerPrefs.GetString(LOGIN_USERID_KEY);
            password = PlayerPrefs.GetString(LOGIN_PASSWORD_KEY);

            // login.

            var loginFuture = profile.LoginFuture(
                new Gs2AccountAuthenticator(
                    profile.Gs2Session,
                    profile.Gs2RestSession,
                    ACCOUNT_NAMESPACE,
                    ACCOUNT_ANGOU_KEY_ID,
                    user_id,
                    password
                )
            );
            yield return loginFuture;
            if (loginFuture.Error != null)
            {
                throw loginFuture.Error;
            }

            Debug.Log($"Login UserId: {user_id}");
            Debug.Log($"Login Password: {password}");

            gameSession = loginFuture.Result;
        }
        else
        {
            // Create anonymous account

            Debug.Log("Create anonymous account");

            var createFuture = gs2Domain.Account.Namespace(
                ACCOUNT_NAMESPACE
            ).Create();
            yield return createFuture;
            if (createFuture.Error != null)
            {
                throw createFuture.Error;
            }

            // Load created account

            var loadFuture = createFuture.Result.Model();
            yield return loadFuture;
            if (loadFuture.Error != null)
            {
                throw loadFuture.Error;
            }
            var account = loadFuture.Result;

            // Dump anonymous account

            Debug.Log($"Create UserId: {account.UserId}");
            Debug.Log($"Create Password: {account.Password}");

            PlayerPrefs.SetString(LOGIN_USERID_KEY, account.UserId);
            PlayerPrefs.SetString(LOGIN_PASSWORD_KEY, account.Password);

            user_id = account.UserId;
            password = account.Password;

            // Log-in created anonymous account

            // Gs2AccountAuthenticator.Gs2AccountAuthenticator(Gs2WebSocketSession, Gs2RestSession, string, string, string, string, GatewaySetting, VersionSetting)

            var loginFuture = profile.LoginFuture(
                new Gs2AccountAuthenticator(
                    profile.Gs2Session,
                    profile.Gs2RestSession,
                    ACCOUNT_NAMESPACE,
                    ACCOUNT_ANGOU_KEY_ID,
                    account.UserId,
                    account.Password
                )
            );
            yield return loginFuture;
            if (loginFuture.Error != null)
            {
                throw loginFuture.Error;
            }
            gameSession = loginFuture.Result;
        }

        // Load TakeOver settings

        var it = gs2Domain.Account.Namespace(
            ACCOUNT_NAMESPACE
        ).Me(
            gameSession
        ).TakeOvers();

        while (it.HasNext())
        {
            yield return it.Next();
            if (it.Error != null)
            {
                throw it.Error;
            }
            if (it.Current != null)
            {
                // Dump TakeOver setting
                Debug.Log($"Type: {it.Current.Type}");
                Debug.Log($"Identifier: {it.Current.UserIdentifier}");
            }
        }

        // Finalize GS2-SDK
        Debug.Log("ログイン完了 UserId " + user_id + " Pass " + password);
        isCompleteLogin = true;

        StartCoroutine(GetMyProfile());

        //yield return profile.Finalize();
    }

    IEnumerator GetMyProfile()
    {
        var domain = gs2Domain.Friend.Namespace(
            FRIEND_NAMESPACE
        ).Me(
            gameSession
        ).Profile(
        );
        var future = domain.Model();
        yield return future;
        Gs2.Unity.Gs2Friend.Model.EzProfile item = future.Result;

        Debug.Log("プロフィール情報取得完了 PublicProfile " + item.PublicProfile);

        Application.appSceneManager.InvokeRefreshUserInfoListener(item.PublicProfile);
    }

    public IEnumerator UploadMyProfile(string newPublicProfile)
    {
        var domain = gs2Domain.Friend.Namespace(
            FRIEND_NAMESPACE
        ).Me(
            gameSession
        ).Profile(
        );
        var future = domain.UpdateProfile(
            newPublicProfile,
            "test",
            "test"
        );
        yield return future;
        if (future.Error != null)
        {
            Debug.Log("プロフィール更新失敗");
            yield break;
        }
        var future2 = future.Result.Model();
        yield return future2;
        if (future2.Error != null)
        {
            Debug.Log("プロフィール更新失敗2");
            yield break;
        }
//        var result = future2.Result;

        Debug.Log("プロフィール更新完了");
    }

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(RefreshList());
        }else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ExecExchange("Exchange002_ClearEntry"));
        }
        */
    }

    IEnumerator ExecExchange(string exchangeName)
    {
        Debug.Log(exchangeName + "を実施します");

        // Exchangeをする.
        {
            var domain = gs2Domain.Exchange.Namespace(
                namespaceName: "Exchange002"
            ).Me(
                gameSession
            ).Exchange(
            );

            var future = domain.Exchange(
                exchangeName,
                1,
                null
            );
            yield return future;
            if (future.Error != null)
            {
                Gs2ClientHolder clientHolder = GameObject.Find("GS2_UIKitSample").GetComponent<Gs2ClientHolder>();
                clientHolder.DebugErrorHandler(future.Error, null);
                yield break;
            }

            var item = future.Result;
        }

        Debug.Log(exchangeName + "が完了しました");
    }


    public IEnumerator RefreshList(UnityAction onCompleteFunc)
    {
        for (int i = 0; i < DefineParam.CHARA_MAX_ID + 1; i++)
        {
            hasCharaFlag[i] = false;
        }


        yield return StartCoroutine(ReLogin());


        // 所持キャラ一覧を取得.
        {

            var domain_dictionary = gs2Domain.Dictionary.Namespace(
                namespaceName: "HasCharaDictionary"
            ).Me(
                gameSession
            ).Entries();

            /*
            var domain_dictionary = gs2Domain.Dictionary.Namespace(
                namespaceName: "HasCharaDictionary"
            ).EntryModels();
            */

            var it_Entries = domain_dictionary;


            List<EzEntry> items = new List<EzEntry>();
            while (it_Entries.HasNext())
            {
                yield return it_Entries.Next();
                if (it_Entries.Error != null)
                {
                    Gs2ClientHolder clientHolder = GameObject.Find("GS2_UIKitSample").GetComponent<Gs2ClientHolder>();
                    clientHolder.DebugErrorHandler(it_Entries.Error, null);
                    break;
                }
                if (it_Entries.Current != null)
                {
                    items.Add(it_Entries.Current);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log(items[i].Name + "を持っている");

                string numString = items[i].Name.Substring(5, 3);
                int charaId = int.Parse(numString);
                hasCharaFlag[charaId] = true;
            }
        }

        if (onCompleteFunc != null)
        {
            onCompleteFunc();
        }
    }

    public bool IsCompleteLogin()
    {
        return isCompleteLogin;
    }

    public bool HasChara(int charaId)
    {
        if (hasCharaFlag == null)
        {
            return false;
        }
        return hasCharaFlag[charaId];
    }

    public int LocalCharaGacha()
    {
        int charaId = Random.Range(1, 14 + 1);
        string exchangeName = "Exchange002_Chara" + charaId.ToString("D3");
        Debug.Log(exchangeName);
        StartCoroutine(ExecExchange(exchangeName));
        return charaId;
    }

    public void ClearCharaFlag()
    {
        StartCoroutine(ExecExchange("Exchange002_ClearEntry"));
    }

    private IEnumerator ReLogin()
    {
        /* 再ログイン処理 同じセッションのままではDictionaryのEntryを取り直せない？ */
        profile.Finalize();

        profile = new Profile(
            CLIENT_ID,
            CLIENT_SECRET,
            reopener: new Gs2BasicReopener()
        );

        // Create GS2 client
        var initializeFuture = profile.InitializeFuture();
        yield return initializeFuture;
        if (initializeFuture.Error != null)
        {
            throw initializeFuture.Error;
        }
        gs2Domain = initializeFuture.Result;

        var loginFuture = profile.LoginFuture(
            new Gs2AccountAuthenticator(
                profile.Gs2Session,
                profile.Gs2RestSession,
                ACCOUNT_NAMESPACE,
                ACCOUNT_ANGOU_KEY_ID,
                user_id,
                password
            )
        );
        yield return loginFuture;
        if (loginFuture.Error != null)
        {
            throw loginFuture.Error;
        }

        gameSession = loginFuture.Result;
        /* 再ログイン処理 同じセッションのままではDictionaryのEntryを取り直せない？ ここまで */
    }
}