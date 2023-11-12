using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaCell : MonoBehaviour
{
    void Awake(){
        nameText = this.transform.Find("CharaName").GetComponent<Text>();
        charaImage = this.transform.Find("CharaImage").GetComponent<Image>();
        notHasCover = this.transform.Find("NotHasCover").GetComponent<Image>();
        notHasCover.color = new Color(0,0,0,0.9f);

        Font font = Resources.Load<Font>("Fonts/keifont");
        nameText.font = font;
    }

    public void RefreshCharaImage(int charaId, bool isNotHave){
        if (Application.fixDataManager == null)
        {
            return;
        }

        nameText.text = Application.fixDataManager.GetCharaName(charaId);
        nameText.color = new Color(0, 0, 255);
        charaImage.sprite = Resources.Load<Sprite>(Application.fixDataManager.GetCharaImagePath(charaId));
        notHasCover.enabled = isNotHave;
    }

    public void HideCharaImage(){
        nameText.text = "";
        charaImage.sprite = Resources.Load<Sprite>("Textures/Chara/NoChara");
        notHasCover.enabled = false;

    }

    Text nameText;
    Image charaImage;
    Image notHasCover;
}
