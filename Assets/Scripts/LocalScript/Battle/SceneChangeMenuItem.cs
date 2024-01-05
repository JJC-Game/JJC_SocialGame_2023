using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeMenuItem : CommonLineMenuItem
{
    Text text;
    DefineParam.SCENE_ID changeSceneId;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
    }

    public override void SetText(string newText)
    {
        text.text = newText;
    }

    public void SetScene(DefineParam.SCENE_ID sceneId)
    {
        changeSceneId = sceneId;
    }

    public override void Click()
    {
        if (Application.IsEnableUIControl())
        {
            Application.appSceneManager.ChangeScene(changeSceneId);
        }
    }
}
