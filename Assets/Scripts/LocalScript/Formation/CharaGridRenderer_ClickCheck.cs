using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaGridRenderer_ClickCheck : CharaGridRenderer
{


    public override void OnMouseButtonDown()
    {
        base.OnMouseButtonDown();

        // 今クリックしたのは誰？
        Vector2 currentMousePosition = Input.mousePosition;
        clickedCharaId = DefineParam.CHARA_INVALID;
        for (int charaId = 0; charaId < GRID_NUM; charaId++)
        {
            Transform trans = this.transform.Find("RectMask/VLayout/HLayout/CharaCell" + charaId.ToString());
            CharaCell charaCell = trans.GetComponent<CharaCell>();

            if (charaCell.IsPointing())
            {
                clickedCharaId = charaId;
            }
        }
    }

    public override void OnMouseButtonUp()
    {
        base.OnMouseButtonUp();

        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 diffPosition = currentMousePosition - holdStartMousePosition;
        float diffLength = diffPosition.magnitude;

        if (diffLength < 20 && clickedCharaId != DefineParam.CHARA_INVALID)
        {
            Debug.Log("チェックマークを入れる");

            Transform trans = this.transform.Find("RectMask/VLayout/HLayout/CharaCell" + clickedCharaId.ToString());
            CharaCell charaCell = trans.GetComponent<CharaCell>();
            charaCell.SetCheckEnable(!charaCell.IsCheckEnable());
        }
    }

    int clickedCharaId;
}
