using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstanceChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InstanceCheck();
    }

    public void InstanceCheck()
    {
        Debug.Log("インスタンス検査を実施");
        GameObject canvasObj = InstanceExistCheck("AppCanvas");
        if (canvasObj != null)
        {
            CanvasScaler sceneCanvasScaler = canvasObj.GetComponent<CanvasScaler>();
            sceneCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            sceneCanvasScaler.referenceResolution = new Vector2(960, 600);
            sceneCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            sceneCanvasScaler.referencePixelsPerUnit = 100;
        }
        InstanceExistCheck("GlobalInstance");
        InstanceExistCheck("FadeManager");
    }

    public GameObject InstanceExistCheck(string instanceName)
    {
        GameObject checkObject;
        checkObject = GameObject.Find(instanceName);
        string assertMessage = "ERROR ERROR ERROR " + instanceName + "が存在しません";
        Debug.Assert(checkObject != null, assertMessage);
        return checkObject;
    }
}
