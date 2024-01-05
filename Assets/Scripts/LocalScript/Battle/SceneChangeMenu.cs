using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeMenu : CommonLineMenu
{
    protected override void Awake()
    {
        base.Awake();
        menuItemPrefab = Resources.Load<GameObject>("Prefabs/Battle/UI/SceneChangeMenuItem");
    }
    // Start is called before the first frame update
    protected override void InterInitItem()
    {
        string[] menuItemString = {
            "1月イベント",
        };

        menuItemArray = new SceneChangeMenuItem[menuItemString.Length];

        for (int itemIndex = 0; itemIndex < menuItemArray.Length; itemIndex++)
        {
            GameObject menuItemInstance = Instantiate(menuItemPrefab);
            menuItemArray[itemIndex] = menuItemInstance.GetComponent<SceneChangeMenuItem>();
            menuItemInstance.transform.SetParent(menuBaseRectTrans);
            menuItemArray[itemIndex].SetText(menuItemString[itemIndex]);

            SceneChangeMenuItem sceneChangeMenuItem = menuItemInstance.GetComponent<SceneChangeMenuItem>();
            sceneChangeMenuItem.SetScene(DefineParam.SCENE_ID.BattleSelect_1gatsu);
        }
    }

    public override void InterOnMouseButtonDown()
    {
        for (int itemIndex = 0; itemIndex < menuItemArray.Length; itemIndex++)
        {
            if (menuItemArray[itemIndex].IsPointing())
            {
                clickedMenuItemId = itemIndex;
            }
        }
    }

    public override void InterOnMouseButtonUp()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 diffPosition = currentMousePosition - holdStartMousePosition;
        float diffLength = diffPosition.magnitude;

        if (diffLength < 20 && clickedMenuItemId != DefineParam.MENU_INVALID)
        {
            menuItemArray[clickedMenuItemId].Click();
        }
    }
}
