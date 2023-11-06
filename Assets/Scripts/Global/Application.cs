using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
    private void Awake()
    {
        // シングルトン制御.
        if (S == null)
        {
            // このゲームで初めてインスタンスが出来る場合.
            S = this;

            // Applicationが存在するインスタンスを破棄しないように.
            DontDestroyOnLoad(gameObject);
        }
        else if(S == this)
        {
            // 自分自身だったら大丈夫.
        }
        else
        {
            // このゲームで2回目以降にインスタンスが出来た.
            Destroy(gameObject);
        }

    }

    // ---------------- Static Section ---------------- //

    static private Application _S;
    static private Application S
    {
        get
        {
            if (_S == null)
            {
                return null;
            }
            return _S;
        }
        set
        {
            if (_S != null)
            {
                Debug.LogError("_Sは既に設定されています.");
            }
            _S = value;
        }
    }

    private FixDataManager _fixDataManager;
    static public FixDataManager fixDataManager
    {
        get
        {
            if (S != null)
            {
                if (S._fixDataManager == null)
                {
                    S._fixDataManager = S.gameObject.AddComponent<FixDataManager>();
                }
                return S._fixDataManager;
            }
            return null;
        }
    }

    private AppSceneManager _appSceneManager;
    static public AppSceneManager appSceneManager
    {
        get
        {
            if (S != null)
            {
                if (S._appSceneManager == null)
                {
                    S._appSceneManager = S.gameObject.AddComponent<AppSceneManager>();
                }
                return S._appSceneManager;
            }
            return null;
        }
    }    

    private UserDataManager _userDataManager;
    static public UserDataManager userDataManager
    {
        get
        {
            if (S != null)
            {
                if (S._userDataManager == null)
                {
                    S._userDataManager = S.gameObject.AddComponent<UserDataManager>();
                }
                return S._userDataManager;
            }
            return null;
        }
    }

    private GS2Manager _gs2Manager;
    static public GS2Manager gs2Manager
    {
        get
        {
            if (S != null)
            {
                if (S._gs2Manager == null)
                {
                    S._gs2Manager = S.gameObject.GetComponent<GS2Manager>();
                }
                return S._gs2Manager;
            }
            return null;
        }
    }

    static public bool IsEnableUIControl()
    {
        if (FadeManager.IsFading() || !gs2Manager.IsCompleteLogin())
        {
            return false;
        }
        return true;
    }
}
