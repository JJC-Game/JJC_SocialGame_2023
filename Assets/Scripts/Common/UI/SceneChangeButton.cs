using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public DefineParam.SCENE_ID changeSceneId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (Application.IsEnableUIControl())
        {
            Application.appSceneManager.ChangeScene(changeSceneId);
        }
    }
}
