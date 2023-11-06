using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Perform_Gacha10
{
    public GameObject instance;
    public Image[] image;
    public Text[] name;
    private Image back;

    const int ITEM_NUM = 10;

    

    public void Init()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Perform/UIParts_Perform_Gacha_10");

        instance = GameObject.Instantiate(prefab);
        RectTransform rectTrans = instance.transform as RectTransform;
        instance.transform.SetParent(GameObject.Find("UIParts_Perform_Anchor").transform);
        rectTrans.anchoredPosition = new Vector2(0, 0);

        image = new Image[ITEM_NUM];
        name = new Text[ITEM_NUM];

        for (int i = 0; i < ITEM_NUM; i++)
        {
            image[i] = instance.transform.Find("VLayout/HLayout/UIParts_Perform_Item_Gacha_10_" + i + "/Image").GetComponent<Image>();
            name[i] = instance.transform.Find("VLayout/HLayout/UIParts_Perform_Item_Gacha_10_" + i + "/Name").GetComponent<Text>();
        }
        back = instance.transform.Find("Back").GetComponent<Image>();

        // イベントトリガーの設定.
        EventTrigger trigger = back.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventDate) => { Deactivate(); });
        trigger.triggers.Add(entry);

        SetActive(false);
    }

    public void SetActive(bool active)
    {
        instance.SetActive(active);
    }

    public void Activate()
    {
        SetActive(true);
    }

    public void Deactivate()
    {
        SetActive(false);
    }

    public void SetName(string inputName, int index)
    {
        name[index].text = inputName;
    }

    public void SetSprite(Sprite inputSprite, int index)
    {
        image[index].sprite = inputSprite;
    }
}
