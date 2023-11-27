using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationRenderer_Position : MonoBehaviour
{
    private void Awake()
    {
        formationRenderer = transform.parent.GetComponent<FormationRenderer>();

        string objectName = "UIParts_CharaCell";
        Debug.Assert(transform.Find(objectName) != null, objectName);
        charaCell = transform.Find(objectName).GetComponent<CharaCell>();

        objectName = "StaticText/SelectBG";
        Debug.Assert(transform.Find(objectName) != null, objectName);
        positionSelectUI = transform.Find(objectName).GetComponent<Image>();

        positionSelectUI.enabled = false;
    }

    public void Init(int positionIndex)
    {
        myPositionIndex = positionIndex;
    }

    public void OnClick()
    {
        // 現在のポジションを選択状態にする.
        formationRenderer.SelectPosition(myPositionIndex);
    }

    public void SetSelectenable(bool enable)
    {
        positionSelectUI.enabled = enable;
    }

    public void ClearSelect()
    {
        positionSelectUI.enabled = false;
    }

    public void SetCharaId(int charaId)
    {
        charaCell.RefreshCharaImage(charaId, false);
    }

    FormationRenderer formationRenderer;
    CharaCell charaCell;
    Image positionSelectUI;
    int myPositionIndex;
}
