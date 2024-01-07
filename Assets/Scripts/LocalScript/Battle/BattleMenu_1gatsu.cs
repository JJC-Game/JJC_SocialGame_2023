using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu_1gatsu : BattleMenu
{
    protected override void Awake()
    {
        base.Awake();
        menuItemPrefab = Resources.Load<GameObject>("Prefabs/Battle/UI/BattleMenuItem");
    }
    protected override void InterInitItem()
    {
        string[] menuItemString = {
            "1月バトル1"
        };

        int[] menuItemBattleId =
        {
            100
        };

        menuItemArray = new BattleMenuItem[menuItemString.Length];

        for (int itemIndex = 0; itemIndex < menuItemArray.Length; itemIndex++)
        {
            GameObject menuItemInstance = Instantiate(menuItemPrefab);
            menuItemArray[itemIndex] = menuItemInstance.GetComponent<BattleMenuItem>();
            menuItemInstance.transform.SetParent(menuBaseRectTrans);
            menuItemArray[itemIndex].SetText(menuItemString[itemIndex]);

            BattleMenuItem battleMenuItem = menuItemInstance.GetComponent<BattleMenuItem>();
            battleMenuItem.SetBattleId(menuItemBattleId[itemIndex]);
        }
    }
}
