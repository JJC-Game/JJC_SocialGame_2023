using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharaGachaManager : MonoBehaviour
{
    const int CHARACTER_NUM = 32;

    Perform_Gacha1 performGacha1;
    Perform_Gacha10 performGacha10;

    int currentGachaCharaId;

    private void Awake()
    {
        performGacha1 = new Perform_Gacha1();
        performGacha10 = new Perform_Gacha10();
        currentGachaCharaId = DefineParam.CHARA_INVALID;
    }
    // Start is called before the first frame update
    void Start()
    {
        performGacha1.Init();
        performGacha10.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickGachaButton()
    {
        currentGachaCharaId = Application.gs2Manager.LocalCharaGacha();
        DrawPerform(currentGachaCharaId);
    }

    public void OnClickGacha10Button()
    {
//        int userId = UserApplication.p3_userDataManager.GetUserId();
//        UserApplication.phpConnectManager.CallPlayGacha10(userId);
    }

    public void DrawPerform(int charaId)
    {
        performGacha1.Activate();
        performGacha1.SetSprite(Resources.Load<Sprite>((Application.fixDataManager.GetCharaImagePath(charaId))));
        if (charaId == 5 || charaId == 10)
        {
            performGacha1.SetSpriteframe(Resources.Load<Sprite>("Textures/RarityFrame/wakusozaiUR"));
        }
        else if (charaId == 4 || charaId == 9 || charaId == 14)
        {
            performGacha1.SetSpriteframe(Resources.Load<Sprite>("Textures/RarityFrame/wakusozaiR"));
        }
        else
        {
            performGacha1.SetSpriteframe(Resources.Load<Sprite>("Textures/RarityFrame/wakusozaiC"));
        }
        performGacha1.SetName(Application.fixDataManager.GetCharaName(charaId));
    }

    public void DrawPerform10(string gachaResult)
    {
/*        string[] gachaResultArray = gachaResult.Split(",");

        performGacha10.Activate();

        for (int i = 0; i < 10; i++)
        {
            FixCharaManager.FixCharaData fixCharaData = UserApplication.fixCharaManager.GetFixCharaData(int.Parse(gachaResultArray[i]));
            performGacha10.SetSprite(Resources.Load<Sprite>(fixCharaData.imagePath), i);
            performGacha10.SetName(fixCharaData.name, i);
        }*/
    }


    public void OnClickResetCharaHasFlag()
    {
        Application.gs2Manager.ClearCharaFlag();
    }

    public int GetCurrentGachaCharaId()
    {
        return currentGachaCharaId;
    }
}
