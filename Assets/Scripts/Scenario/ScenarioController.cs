using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScenarioController : MonoBehaviour
{
    public int m_currentScenarioScriptRowIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_currentScenarioScriptRowIndex = 0;
        SetRowIndex(m_currentScenarioScriptRowIndex);
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetMouseButtonDown(0) && Application.IsEnableUIControl()){
            m_currentScenarioScriptRowIndex++;
            SetRowIndex(m_currentScenarioScriptRowIndex);
        }
    }

    void SetRowIndex(int rowIndex){
        ScenarioScriptLoader scenarioScriptLoader = GetComponent<ScenarioScriptLoader>();

        if(!scenarioScriptLoader.IsValidRowIndex(m_currentScenarioScriptRowIndex)){
            // シナリオが一番最後まで終わったら、事前に指定しておいた、シナリオを抜けた後に呼ぶ関数を呼び出す.
            UnityAction scenarioPopOutCallbackFunc = Application.appSceneManager.GetScenarioPopOutCallbackFunc();
            if(scenarioPopOutCallbackFunc != null){
                scenarioPopOutCallbackFunc();
            }
            return;
        }

        ScenarioScriptLoader.ScenarioScriptRow scenarioScriptRow = scenarioScriptLoader.GetScenarioScriptRow(m_currentScenarioScriptRowIndex);

        {
            // コマンド無しの、通常のセリフ.
            string talkStr = scenarioScriptRow.charaTalkText;

            // 置換.
            talkStr = talkStr.Replace("{USERNAME}",Application.userDataManager.GetUserName());
            talkStr = talkStr.Replace("{BR}","\n");

            GameObject.Find("TextPlate/Text").GetComponent<Text>().text = talkStr;

            int talkChara = scenarioScriptRow.charaId;
            Debug.Log("セリフ キャラID" + talkChara.ToString());
            string charaName = Application.fixDataManager.GetCharaName(talkChara);

            GameObject.Find("NamePlate/Text").GetComponent<Text>().text = charaName;

            string charaImagePath = Application.fixDataManager.GetCharaImagePath(talkChara);

             GameObject.Find("CenterCharaSpritePos/Sprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(charaImagePath);
        }
    }
}
