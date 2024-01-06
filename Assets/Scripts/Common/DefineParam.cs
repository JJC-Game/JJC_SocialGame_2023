using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineParam : MonoBehaviour
{
    public const int SCENARIO_INVALID = 0;
    public const int CHARA_INVALID = 0;
    public const int CHARA_MIN_ID  = 1;
    public const int CHARA_MAX_ID = 14;
    public const int MENU_INVALID = -1;

    public const int START_SCENE_ID = (int)SCENE_ID.Lobby;
    public const int END_SCENE_ID = (int)SCENE_ID.Num;

    public enum SCENE_ID{
        Lobby = 0,
        Talk,
        Gacha,
        Title,
        System,
        Battle,
        Result,
        BattleSelect,
        BattleSelect_1gatsu,
        Num,
        Invalid,
    }

    public enum BATTLE_WAVE
    {
        WAVE_1 = 0,
        WAVE_2 = 1,
        WAVE_3 = 2,
        END_WAVE = WAVE_3
    }
}
