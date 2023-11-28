using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharaGridRenderer_Formation : CharaGridRenderer
{
    public override void OnMouseButtonDown()
    {
        base.OnMouseButtonDown();

        // 今クリックしたのは誰？
        Vector2 currentMousePosition = Input.mousePosition;
        clickedCharaId = DefineParam.CHARA_INVALID;
        for (int charaId = 0; charaId < GRID_NUM; charaId++)
        {
            if (charaCellArray[charaId].IsPointing())
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
            selectCharaInvokeEvent.Invoke();
        }
    }

    public void AddListener(UnityAction action)
    {
        selectCharaInvokeEvent.AddListener(action);
    }


    public void RemoveListener(UnityAction action)
    {
        selectCharaInvokeEvent.RemoveListener(action);
    }

    public int GetClickedCharaId()
    {
        return clickedCharaId;
    }

    public void ClearCheck()
    {
        for (int charaId = 0; charaId < GRID_NUM; charaId++)
        {
            SetCheckEnable(charaId, false);
        }
    }

    public void SetCheckEnable(int charaId, bool enable)
    {
        charaCellArray[charaId].SetCheckEnable(enable);
    }

    int clickedCharaId;
    UnityEvent selectCharaInvokeEvent = new UnityEvent();
}
