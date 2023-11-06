using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineParam : MonoBehaviour
{
    public const int SCENARIO_INVALID = 0;
    public const int CHARA_INVALID = 0;
    public const int CHARA_MIN_ID  = 1;
    public const int CHARA_MAX_ID = 14;

    public enum SCENE_ID{
        Lobby,
        Talk,
        Gacha
    }    
}
