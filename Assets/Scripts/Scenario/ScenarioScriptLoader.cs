using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScenarioScriptLoader : MonoBehaviour
{
    void Awake(){
        scenarioId = Application.appSceneManager.GetScenarioId();

        if(scenarioId == DefineParam.SCENARIO_INVALID){
            scenarioId = debugPlayScenarioId;
        }

        LoadScript();        
    }

    void Start(){

    }

    private void LoadScript(){
        string path = "FixData/Scenario/Scenario" + ((int)scenarioId).ToString("000") + ".xlsm_Scenario";

        TextAsset csvFile;
        m_scenarioScriptRowList = new List<ScenarioScriptRow>();

        csvFile = Resources.Load<TextAsset>(path);
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            ScenarioScriptRow currentRow = new ScenarioScriptRow();
            string line = reader.ReadLine();
            currentRow.rawText = line;

            string[] lineArray = line.Split(',');
            currentRow.charaId = int.Parse(lineArray[0]);
            currentRow.charaTalkText = lineArray[1];
            m_scenarioScriptRowList.Add(currentRow);
        }
    }

    void PrintDebugLog(ScenarioScriptRow row){
        Debug.Log(row.rawText);
    }

    public ScenarioScriptRow GetScenarioScriptRow(int rowIndex){
        return m_scenarioScriptRowList[rowIndex];
    }

    public bool IsValidRowIndex(int rowIndex){
        if(m_scenarioScriptRowList == null){
            return false;
        }

        if(rowIndex < 0 || rowIndex >= m_scenarioScriptRowList.Count){
            return false;
        }
        return true;
    }

    public struct ScenarioScriptRow{
        public string rawText; // 生テキスト.
        public int charaId;
        public string charaTalkText;
    }

    int scenarioId;
    List<ScenarioScriptRow> m_scenarioScriptRowList = null;

    public int debugPlayScenarioId;
}
