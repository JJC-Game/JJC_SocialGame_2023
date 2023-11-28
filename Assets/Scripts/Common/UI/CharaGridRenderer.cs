using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharaGridRenderer : MonoBehaviour
{
    bool mouseHold;
    protected Vector2 holdStartMousePosition;
    Vector2 currentGridPosition;
    RectTransform gridMenuRectTrans;

    protected const int GRID_NUM = 33;

    const int MIN_Y_POSITION = -400;
    const int MAX_Y_POSITION = 400;

    protected CharaCell[] charaCellArray;

    void Awake()
    {
        mouseHold = false;

        gridMenuRectTrans = this.transform.Find("RectMask/VLayout").transform as RectTransform;
        currentGridPosition = gridMenuRectTrans.anchoredPosition;

        charaCellArray = new CharaCell[GRID_NUM];
        for (int charaId = 0; charaId < GRID_NUM; charaId++)
        {
            string objectName = "RectMask/VLayout/HLayout/CharaCell" + charaId.ToString();
            Debug.Assert(transform.Find(objectName) != null, objectName);
            Transform trans = this.transform.Find(objectName);
            charaCellArray[charaId] = trans.GetComponent<CharaCell>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        RefreshGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseHold)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 diffPosition = currentMousePosition - holdStartMousePosition;

            float yPosition = currentGridPosition.y + diffPosition.y;

            if (yPosition < MIN_Y_POSITION)
            {
                yPosition = MIN_Y_POSITION;
            }
            if (yPosition > MAX_Y_POSITION)
            {
                yPosition = MAX_Y_POSITION;
            }

            gridMenuRectTrans.anchoredPosition = new Vector2(currentGridPosition.x, yPosition);
        }
    }

    public virtual void OnMouseButtonDown()
    {
        mouseHold = true;
        holdStartMousePosition = Input.mousePosition;
    }

    public virtual void OnMouseButtonUp()
    {
        mouseHold = false;

        currentGridPosition = gridMenuRectTrans.anchoredPosition;
    }

    public void OnClickRefreshButton()
    {
        StartCoroutine(Application.gs2Manager.RefreshList(RefreshGrid));
    }

    virtual public void RefreshGrid()
    {
        for (int charaId = 0; charaId < GRID_NUM; charaId++)
        {
            if (DefineParam.CHARA_MIN_ID <= charaId && charaId <= DefineParam.CHARA_MAX_ID)
            {
                bool isNotHaveChara = !Application.gs2Manager.HasChara(charaId);
                charaCellArray[charaId].RefreshCharaImage(charaId, isNotHaveChara);
            }
            else
            {
                charaCellArray[charaId].HideCharaImage();
            }

        }
    }
}
