using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : CommonLineMenu
{
    protected override void Awake()
    {
        base.Awake();
        menuItemPrefab = Resources.Load<GameObject>("Prefabs/Battle/UI/BattleMenuItem");
    }
    // Start is called before the first frame update
    protected override void InterInitItem()
    {
        string[] menuItemString = {
            "バトル1",
            "バトル2",
            "バトル3",
            "バトル4",
            "バトル5",
        };

        int[] menuItemBattleId =
        {
            1,2,3,4,5
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
