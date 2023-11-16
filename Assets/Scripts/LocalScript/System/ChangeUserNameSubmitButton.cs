using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUserNameSubmitButton : MonoBehaviour
{
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
        string newUserName = transform.parent.transform.Find("InputField (Legacy)").GetComponent<InputField>().text;
        Application.userDataManager.SetUserName(newUserName);
        Application.userDataManager.UploadProfile();
    }
}
