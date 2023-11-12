using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// FadeManagerは、独立したgameObjectにする.
// 自分用のCallbackTimerを持つ.
public class FadeManager : MonoBehaviour {
    public static FadeManager m_Instance
    {
        get; private set;
    }

    public static FadeManager GetInstance()
    {
        if(m_Instance == null)
        {
            // ヒエラルキーの中から、FadeManagerクラスを持ったgameObjectを探す.
            m_Instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

            if (m_Instance == null)
            {
                Debug.LogError(typeof(FadeManager) + "is nothing");
            }
        }
        return m_Instance;
    }

    public static void Create()
    {
        if (m_Instance == null)
        {
            // ヒエラルキーの中に、FadeManagerを新しく作る.
            GameObject obj = new GameObject("FadeManager");
            m_Instance = obj.AddComponent<FadeManager>();
        }
    }


    void Awake()
    {
        if(m_Instance != null)
        {
            // 破棄しない設定になったオブジェクトが、2週目以降に生成されたらそのまま破棄.
            // つまり、残るのは１つめのオブジェクト？.
            Destroy(gameObject);
            return;
        }
        m_Instance = this;

//        m_globalFadePanel = GameObject.Find("GlobalFadePanel").GetComponent<Image>();
        m_pCallbackTimer = GetComponent<CallbackTimer>();
        if(m_pCallbackTimer == null){
            m_pCallbackTimer = this.gameObject.AddComponent<CallbackTimer>();
        }
        // 自分自身を破棄しない設定に.
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        // インスタンスを生成
        if (m_unityEvent == null)
            m_unityEvent = new UnityEvent();

        // 開始時点で、フェード状態にすること.
        m_eCurrentState = E_FADE_STATE.eON_FADE;
        SetFadeIn(1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_eCurrentType == E_FADE_TYPE.eINVALID_FADE) { return; }

        float fRatio = 0.0f;

        switch (m_eCurrentState)
        {
            case E_FADE_STATE.eNO_FADE:
                fRatio = 0.0f;
                break;
            case E_FADE_STATE.eON_FADE:
                fRatio = 1.0f;
                break;
            case E_FADE_STATE.eFADE_OUT:
                fRatio = m_pCallbackTimer.GetRate();
                fRatio = fRatio * fRatio;
                break;
            case E_FADE_STATE.eFADE_IN:
                fRatio = 1.0f - m_pCallbackTimer.GetRate();
                fRatio = fRatio * fRatio;
                break;
            default:
                break;
        }

//        m_globalFadePanelColor.a = fRatio;
        m_globalFadePanelColor = Color.Lerp(Color.clear, Color.black, fRatio);
    }

    void SetFadeOut(E_FADE_TYPE type, float fFadeTime = FADE_TIME, UnityAction pFunc = null, UnityAction pFunc2 = null)
    {
        if (m_eCurrentState != E_FADE_STATE.eNO_FADE)
        {
            return; // 現在状態が欲しいやつじゃなかったら、処理に入らない.
        }

        m_eCurrentType = type;
        m_eCurrentState = E_FADE_STATE.eFADE_OUT;
        m_pCallbackTimer.ResetTimer(fFadeTime, () => SetOnFade());

        if (pFunc != null)
        {
            m_unityEvent.AddListener(pFunc);
        }
        if (pFunc2 != null)
        {
            m_unityEvent.AddListener(pFunc2);
        }
    }

    void SetOnFade()
    {
        if (m_eCurrentState != E_FADE_STATE.eFADE_OUT)
        {
            Debug.Assert(m_eCurrentState == E_FADE_STATE.eFADE_OUT);
            return; // 現在状態が欲しいやつじゃなかったら、処理に入らない.
        }
        m_eCurrentState = E_FADE_STATE.eON_FADE;

        m_unityEvent.Invoke();
        m_unityEvent.RemoveAllListeners();
    }

    void SetFadeIn(float fFadeTime = FADE_TIME, UnityAction pFunc = null)
    {
        if (m_eCurrentState != E_FADE_STATE.eON_FADE)
        {
            Debug.Assert(m_eCurrentState == E_FADE_STATE.eON_FADE);
            return; // 現在状態が欲しいやつじゃなかったら、処理に入らない.
        }
        m_eCurrentState = E_FADE_STATE.eFADE_IN;
        m_pCallbackTimer.ResetTimer(fFadeTime, () => SetNoFade());

        if (pFunc != null)
        {
            m_unityEvent.AddListener(pFunc);
        }
    }

    void SetNoFade()
    {
        if (m_eCurrentState != E_FADE_STATE.eFADE_IN)
        {
            Debug.Assert(m_eCurrentState == E_FADE_STATE.eFADE_IN);
            return; // 現在状態が欲しいやつじゃなかったら、処理に入らない.
        }
        m_eCurrentType = E_FADE_TYPE.eINVALID_FADE;
        m_eCurrentState = E_FADE_STATE.eNO_FADE;

        m_unityEvent.Invoke();
        m_unityEvent.RemoveAllListeners();
    }

    public void OnGUI()
    {
        // Fade .
        {
            //色と透明度を更新して白テクスチャを描画 .
            GUI.color = this.m_globalFadePanelColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    static public void ChangeScene(string strSceneName)
    {
        FadeManager.GetInstance().SetFadeOut(E_FADE_TYPE.eGLOBAL_FADE, FADE_TIME, () => ChangeSceneCallback(strSceneName));
    }

    static public void ChangeSceneCallback(string strSceneName)
    {
        SceneManager.LoadScene(strSceneName);
        FadeManager.GetInstance().SetFadeIn();
    }

    static public void ChangeState(UnityAction pFunc = null){
        FadeManager.GetInstance().SetFadeOut(E_FADE_TYPE.eGLOBAL_FADE, FADE_TIME, () => ChangeStateCallback(), pFunc);
    }

    static public void ChangeStateCallback(){
        FadeManager.GetInstance().SetFadeIn();
    }

    static public string GetString(){
        return FadeManager.GetInstance().m_eCurrentState.ToString();
    }

    static public bool IsFading(){
        return FadeManager.GetInstance().m_eCurrentState != E_FADE_STATE.eNO_FADE;
    }

        // メンバ変数.
    Image m_globalFadePanel;
    CallbackTimer m_pCallbackTimer;

    enum E_FADE_STATE
    {
        eNO_FADE,
        eFADE_OUT,
        eON_FADE,
        eFADE_IN,
    }

    E_FADE_STATE m_eCurrentState;

    enum E_FADE_TYPE
    {
        eGLOBAL_FADE,
        eINVALID_FADE
    }

    E_FADE_TYPE m_eCurrentType;

    const float FADE_TIME = 0.5f;
    public const float CHANGE_STATE_FADE_TIME = 0.3f;

    public UnityEvent m_unityEvent; // フェードが終わったとき、何か実行したりする.

    public Color m_globalFadePanelColor = Color.black;
}
