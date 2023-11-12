using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    const string PUBLICPROFILE_KEY_USERNAME = "USERNAME";

    string userName;
    // Start is called before the first frame update
    void Start()
    {
        userName = "ロード中";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshProfile(string publicProfile)
    {
        decodePublicProfile(publicProfile);
    }

    public void decodePublicProfile(string publicProfile)
    {
        if (publicProfile == null)
        {
            return;
        }

        string[] strArray = publicProfile.Split(',');
        for (int i = 0; i < strArray.Length; i++)
        {
            string[] strElement = strArray[i].Split(":");

            switch (strElement[0])
            {
                case PUBLICPROFILE_KEY_USERNAME:
                    userName = strElement[1];
                    break;
            }
        }
    }

    public string encodePublicProfile()
    {
        string publicProfile = "";

        publicProfile += PUBLICPROFILE_KEY_USERNAME + ":" + userName + ",";

        return publicProfile;
    }

    public void SetUserName(string inputUserName)
    {
        userName = inputUserName;
    }

    public string GetUserName()
    {
        return userName;
    }
}
