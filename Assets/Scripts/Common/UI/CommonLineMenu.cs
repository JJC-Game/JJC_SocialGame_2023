using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonLineMenu : MonoBehaviour
{
    protected bool mouseHold;
    protected Vector2 holdStartMousePosition;
    protected Vector2 currentMenuPosition;
    protected RectTransform menuBaseRectTrans;
    protected int clickedMenuItemId = 0;


    protected int MIN_Y_POSITION = -75;
    protected int MAX_Y_POSITION = 75;
    protected int RENDER_MENU_ITEM_NUM = 3;

    protected const int MENU_ITEM_MARGIN = 10;

    protected GameObject menuItemPrefab;
    protected CommonLineMenuItem[] menuItemArray;

    protected virtual void Awake()
    {
        mouseHold = false;

        menuBaseRectTrans = this.transform.Find("RectMask/MenuBase").transform as RectTransform;
        

        /*
        charaCellArray = new CharaCell[GRID_NUM];
        for (int charaId = 0; charaId < GRID_NUM; charaId++)
        {
            string objectName = "RectMask/VLayout/HLayout/CharaCell" + charaId.ToString();
            Debug.Assert(transform.Find(objectName) != null, objectName);
            Transform trans = this.transform.Find(objectName);
            charaCellArray[charaId] = trans.GetComponent<CharaCell>();
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        InitItem();
        Refresh();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (mouseHold)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 diffPosition = currentMousePosition - holdStartMousePosition;

            float yPosition = currentMenuPosition.y + diffPosition.y;

            if (yPosition < MIN_Y_POSITION)
            {
                yPosition = MIN_Y_POSITION;
            }
            if (yPosition > MAX_Y_POSITION)
            {
                yPosition = MAX_Y_POSITION;
            }

            menuBaseRectTrans.anchoredPosition = new Vector2(currentMenuPosition.x, yPosition);
        }
    }

    public virtual void OnMouseButtonDown()
    {
        mouseHold = true;
        holdStartMousePosition = Input.mousePosition;
        currentMenuPosition = menuBaseRectTrans.anchoredPosition;

        InterOnMouseButtonDown();
    }

    public virtual void InterOnMouseButtonDown()
    {

    }

    public virtual void OnMouseButtonUp()
    {
        mouseHold = false;

        currentMenuPosition = menuBaseRectTrans.anchoredPosition;

        InterOnMouseButtonUp();
    }
    public virtual void InterOnMouseButtonUp()
    {

    }

    virtual public void Refresh()
    {
        
    }

    private void InitItem()
    {
        // 先にメニュー要素の配列を生成する.
        InterInitItem();

        // 次に、生成されたメニュー要素を元に諸々の大きさとかを決める
        RectTransform menuItemPrefabRectTrans = menuItemPrefab.transform as RectTransform;

        menuBaseRectTrans.sizeDelta = new Vector2(menuItemPrefabRectTrans.sizeDelta.x + MENU_ITEM_MARGIN * 2, (menuItemPrefabRectTrans.sizeDelta.y + MENU_ITEM_MARGIN * 2) * menuItemArray.Length);

        if (menuItemArray.Length <= RENDER_MENU_ITEM_NUM)
        {
            MIN_Y_POSITION = -(int)((menuItemArray.Length - RENDER_MENU_ITEM_NUM) * (menuItemPrefabRectTrans.sizeDelta.y + MENU_ITEM_MARGIN * 2) / 2);
            MAX_Y_POSITION = MIN_Y_POSITION;
        }
        else {
            MIN_Y_POSITION = -(int)((menuItemArray.Length - RENDER_MENU_ITEM_NUM) * (menuItemPrefabRectTrans.sizeDelta.y + MENU_ITEM_MARGIN * 2) / 2);
            MAX_Y_POSITION = (int)((menuItemArray.Length - RENDER_MENU_ITEM_NUM) * (menuItemPrefabRectTrans.sizeDelta.y + MENU_ITEM_MARGIN * 2) / 2);
        }

        menuBaseRectTrans.anchoredPosition = new Vector2(0, MIN_Y_POSITION);
    }

    protected virtual void InterInitItem()
    {

    }
}
