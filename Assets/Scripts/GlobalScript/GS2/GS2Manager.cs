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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Gs2Formation.Model;
using Gs2.Gs2Formation.Request;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GS2Manager : MonoBehaviour
{
    const string CLIENT_ID = "GKITg_ndOnKcFmHHmXNi3OjII7vK0tu7lTZAjbrQCTm2KWw_Qg_CMW5HGBkKVwKdgaXE_ueEXZBUI4q1NNpDUQKvQ==";
    const string CLIENT_SECRET = "ezrNGluCeYhTODHwyZLlHvfmAOboPHJW";

    const string ACCOUNT_NAMESPACE = "Player";
    const string ACCOUNT_ANGOU_KEY_ID = "grn:gs2:{region}:{ownerId}:key:account-encryption-key-namespace:key:account-encryption-key";

    const string LOGIN_USERID_KEY = "LoginUserId";
    const string LOGIN_PASSWORD_KEY = "LoginPassword";

    const string FRIEND_NAMESPACE = "PlayerProfile001";

    const string FRIEND_NAMESPACE_FORMATION = "PlayerFormation";

    private Profile profile;
    Gs2Domain gs2Domain;
    GameSession gameSession;

    string user_id;
    string password;

    bool[] hasCharaFlag;


    // 接続状況の管理.
    bool isCompleteLogin;
    bool isConnecting;
    GameObject loginFadeInstance = null;

    // 接続を開始する. 他の接続は行えないようにする.
    private bool StartConnect()
    {
        if (IsConnecting())
        {
            Debug.Log("現在接続中");
            return false;
        }
        isConnecting = true;
        return true;
    }

    // 接続を完了する.他の接続を行ってよくなる.
    private void CompleteConnect()
    {
        isConnecting = false;
    }

    // 現在接続中.
    private bool IsConnecting()
    {
        return isConnecting;
    }

    // ログイン処理が終わっている.
    private bool IsCompleteLogin()
    {
        return isCompleteLogin;
    }

    public bool IsEnableUIControl()
    {
        return !IsConnecting() && IsCompleteLogin();
    }

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
        if (SceneManager.GetActiveScene().name == "Title")
        {
            StartCoroutine(InitializeGS2());
        }
        else
        {
            DummyLogin();
        }
    }

    void DummyLogin()
    {
        isCompleteLogin = true;
        isConnecting = false;
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

        isCompleteLogin = false;
        isConnecting = false;
        yield return profile.Finalize();
    }

    IEnumerator Login()
    {
        // ログインを示すUIを表示.
        GameObject loginFadePrefab = Resources.Load<GameObject>("Prefabs/Title/LoginFade");
        loginFadeInstance = Instantiate(loginFadePrefab);
        loginFadeInstance.transform.SetParent(Application.appCanvas.transform);
        RectTransform rectTrans = loginFadeInstance.transform as RectTransform;
        rectTrans.anchoredPosition = Vector2.zero;

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

        Debug.Log("ログイン完了 UserId " + user_id + " Pass " + password);

        // ユーザー名を取得しておく.
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

        isCompleteLogin = true;
        Destroy(loginFadeInstance);
    }

    IEnumerator GetMyProfile()
    {
        Debug.Assert(StartConnect());

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

        CompleteConnect();
    }

    public IEnumerator UploadMyProfile(string newPublicProfile)
    {
        Debug.Assert(StartConnect());

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

        CompleteConnect();
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
        Debug.Assert(StartConnect());
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
        CompleteConnect();
    }


    public IEnumerator RefreshList(UnityAction onCompleteFunc)
    {
        Debug.Assert(StartConnect());
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

        CompleteConnect();
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

    public IEnumerator GetForm(int formIndex)
    {
        if (gs2Domain == null)
        {
            Debug.Assert(false, "gs2 null");
        }
        if (gameSession == null)
        {
            Debug.Assert(false, "gameSession null");
        }

        var domain_formation_mold_form = gs2Domain.Formation.Namespace(
            "Formation001"
        ).Me(
            gameSession
        ).Mold(
            "Mold001"
        ).Form(
            formIndex
        );

        var future = domain_formation_mold_form.Model();
        yield return future;
        EzForm form = future.Result;

        List<EzSlot> slotList = form.Slots;

        Debug.Log("CompleteGetForm");
        for (int i = 0; i < slotList.Count; i++)
        {
            EzSlot slot = slotList[i];
            Debug.Log("slotName " + slot.Name + ", slotPropertyId " + slot.PropertyId);
        }
    }

    public IEnumerator SetForm(int formIndex, int slot1_charaId, int slot2_charaId, int slot3_charaId)
    {
        if ( gs2Domain == null )
        {
            Debug.Assert(false, "gs2 null");
        }
        if (  gameSession == null)
        {
            Debug.Assert(false, "gameSession null");
        }

        // 署名を得る.
        var domain_dictionary = gs2Domain.Dictionary.Namespace(
            "HasCharaDictionary"
        ).Me(
            gameSession
        ).Entry(
            "Chara001"
        );

        var future_dic_chara001 = domain_dictionary.GetEntryWithSignature(
            ACCOUNT_ANGOU_KEY_ID
        );
        yield return future_dic_chara001;
        if (future_dic_chara001.Error != null)
        {
            Debug.Assert(false, "GetEntryWithSignature Future " + future_dic_chara001.Error.Message);
            yield break;
        }
        var result_dic_chara001 = future_dic_chara001.Result;
        var body_dic_chara001 = future_dic_chara001.Result.Body;
        var signature_dic_chara001 = future_dic_chara001.Result.Signature;

        // SetFormを始める.
        var domain_formation_mold_form = gs2Domain.Formation.Namespace(
            "Formation001"
        ).Me(
            gameSession
        ).Mold(
            "Mold001"
        ).Form(
            0
        );

        Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature[] slotArray;
        slotArray = new Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature[3];
        slotArray[0] = new Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature();
        slotArray[0].Name = "Position001";
        slotArray[0].PropertyType = "gs2_dictionary";
        slotArray[0].Body = "body";
        slotArray[0].Signature = "signature";

        slotArray[1] = new Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature();
        slotArray[1].Name = "Position002";
        slotArray[1].PropertyType = "gs2_dictionary";
        slotArray[1].Body = "body";
        slotArray[1].Signature = "signature";

        slotArray[2] = new Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature();
        slotArray[2].Name = "Position003";
        slotArray[2].PropertyType = "gs2_dictionary";
        slotArray[2].Body = "body";
        slotArray[2].Signature = "signature";


        var future_SetForm = domain_formation_mold_form.SetForm(
            slotArray,
            ACCOUNT_ANGOU_KEY_ID
        );
        yield return future_SetForm;
        if (future_SetForm.Error != null)
        {
            Debug.Assert(false, "SetForm Future " + future_SetForm.Error.Message);
            yield break;
        }
        var future2 = future_SetForm.Result.Model();
        yield return future2;
        if (future2.Error != null)
        {
            Debug.Assert(false, "SetForm Future2 " + future2.Error.Message);
            yield break;
        }
        var result = future2.Result;


        List<EzSlot> slotList = result.Slots;

        Debug.Log("CompleteSetForm");
        for (int i = 0; i < slotList.Count; i++)
        {
            EzSlot slot = slotList[i];
            Debug.Log("slotName " + slot.Name + ", slotPropertyId " + slot.PropertyId);
        }
    }


    public IEnumerator GetPlayerFormation()
    {
        Debug.Assert(StartConnect());
        var domain = gs2Domain.Friend.Namespace(
            FRIEND_NAMESPACE_FORMATION
        ).Me(
            gameSession
        ).Profile(
        );
        var future = domain.Model();
        yield return future;
        Gs2.Unity.Gs2Friend.Model.EzProfile profile_formation = future.Result;

        Debug.Log("編成情報を取得 PublicProfile " + profile_formation.PublicProfile);

        if (profile_formation.PublicProfile == null)
        {
            
        }
        else
        {
            string[] strKeyValue = profile_formation.PublicProfile.Split(":");
            if (strKeyValue.Length == 2)
            {
                string[] strFormationSlotArray = strKeyValue[1].Split(",");
                for (int i = 0; i < Formation.FORMATION_POSITION_NUM; i++)
                {
                    string slot = strFormationSlotArray[i];
                    Debug.Log("charaId " + slot);
                }
            }
        }
        CompleteConnect();
    }

    public IEnumerator SetPlayerFormation(Formation formation)
    {
        Debug.Assert(StartConnect());
        string strSerializedFormation = "formation001:" + formation.GetCharaId(0).ToString() + "," + formation.GetCharaId(1).ToString() + "," + formation.GetCharaId(2).ToString();


        var domain = gs2Domain.Friend.Namespace(
            FRIEND_NAMESPACE_FORMATION
        ).Me(
            gameSession
        ).Profile(
        );
        var future = domain.UpdateProfile(
            strSerializedFormation,
            "test",
            "test"
        );
        yield return future;
        if (future.Error != null)
        {
            Debug.Log("編成情報更新失敗");
            yield break;
        }
        var future2 = future.Result.Model();
        yield return future2;
        if (future2.Error != null)
        {
            Debug.Log("編成情報更新失敗2");
            yield break;
        }
        //        var result = future2.Result;

        Debug.Log("編成情報更新完了");
        CompleteConnect();
    }
}