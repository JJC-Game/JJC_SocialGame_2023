using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleMenuItem : CommonLineMenuItem
{
    Text text;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
    }

    public override void SetText(string newText)
    {
        text.text = newText;
    }

    public override void Click()
    {
        Debug.Log(text.text + "くりっくした");
    }
}
