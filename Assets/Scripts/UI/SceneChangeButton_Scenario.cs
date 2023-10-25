using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton_Scenario : MonoBehaviour
{
    public DefineParam.SCENE_ID changeSceneId;    
    public int scenarioId;

    public void OnClick()
    {
        if (Application.IsEnableUIControl())
        {
            Application.appSceneManager.SetScenarioId(scenarioId);
            Application.appSceneManager.ChangeScene(changeSceneId);
        }
    }
}
