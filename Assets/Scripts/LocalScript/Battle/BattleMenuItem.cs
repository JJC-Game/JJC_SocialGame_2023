using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleMenuItem : CommonLineMenuItem
{
    Text text;
    int clickPlayBattleId;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
    }

    public override void SetText(string newText)
    {
        text.text = newText;
    }

    public void SetBattleId(int battleId)
    {
        clickPlayBattleId = battleId;
    }

    public override void Click()
    {
        Debug.Log(text.text + "くりっくした");

        if (Application.IsEnableUIControl())
        {
            Application.appSceneManager.SetBattleId(clickPlayBattleId);
            Application.appSceneManager.SetBattleWave(DefineParam.BATTLE_WAVE.WAVE_1);
            Application.appSceneManager.ChangeScene(DefineParam.SCENE_ID.Battle);
        }

    }
}
