using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    GameObject userInfo_prefab;
    GameObject userInfo_instance;
    Text userNameText;

    private void Start()
    {
        userInfo_prefab = Resources.Load<GameObject>("Prefabs/Title/UserInfo");

        userInfo_instance = Instantiate(userInfo_prefab);
        userInfo_instance.transform.SetParent(Application.appCanvas.transform);
        RectTransform rectTransform = (RectTransform)userInfo_instance.transform;
        rectTransform.anchoredPosition = new Vector3(0, -50, 0);

        userNameText = userInfo_instance.transform.Find("UserNameText").GetComponent<Text>();
        userNameText.text = "ロード中";

        Application.appSceneManager.AddRefreshUserInfoListener(Refresh);
    }

    private void OnDestroy()
    {
        if (Application.appSceneManager != null)
        {
            Application.appSceneManager.RemoveRefreshUserInfoListener(Refresh);
        }
        Destroy(userInfo_instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Refresh()
    {
        userNameText.text = Application.userDataManager.GetUserName();
    }
}
