using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharaCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    void Awake(){
        nameText = this.transform.Find("CharaName").GetComponent<Text>();
        charaImage = this.transform.Find("CharaImage").GetComponent<Image>();
        notHasCover = this.transform.Find("NotHasCover").GetComponent<Image>();
        notHasCover.color = new Color(0, 0, 0, 0.9f);
        check = this.transform.Find("Check").GetComponent<Image>();
        SetCheckEnable(false);

        Font font = Resources.Load<Font>("Fonts/keifont");
        nameText.font = font;

        isPointing = false;
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

    public void SetCheckEnable(bool isEnable)
    {
        check.enabled = isEnable;
    }

    public bool IsCheckEnable()
    {
        return check.enabled;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("in");
        isPointing = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("out");
        isPointing = false;
    }

    public bool IsPointing()
    {
        return isPointing;
    }

    Text nameText;
    Image charaImage;
    Image notHasCover;
    Image check;
    bool isPointing;
}
