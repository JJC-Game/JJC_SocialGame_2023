using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    const string PUBLICPROFILE_KEY_USERNAME = "USERNAME";

    string userName;

    public const int FORMATION_NUM = 3;

    public class Formation
    {
        int[] charaIdArray;

        public void Init()
        {
            charaIdArray = new int[FORMATION_NUM];
            for (int i = 0; i < FORMATION_NUM; i++)
            {
                charaIdArray[i] = 0;
            }
        }

        public void SetChara(int index, int charaId)
        {
            charaIdArray[index] = charaId;
        }

        public int GetCharaId(int index)
        {
            return charaIdArray[index];
        }
    }

    Formation formation;

    // Start is called before the first frame update
    void Awake()
    {
        userName = "ロード中";
        formation = new Formation();
        formation.Init();
        formation.SetChara(0,1);
        formation.SetChara(1, 2);
        formation.SetChara(2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshProfile(string publicProfile)
    {
        DecodePublicProfile(publicProfile);
    }

    public void DecodePublicProfile(string publicProfile)
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

    public void UploadProfile()
    {
        string newPublicProfile = EncodePublicProfile();

        StartCoroutine(Application.gs2Manager.UploadMyProfile(newPublicProfile));
    }

    public string EncodePublicProfile()
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

    public Formation GetFormation()
    {
        return formation;
    }

}
