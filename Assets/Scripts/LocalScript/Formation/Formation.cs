using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation
{
    public const int FORMATION_POSITION_NUM = 3;
    int[] positionCharaIdArray;

    public void Init()
    {
        positionCharaIdArray = new int[FORMATION_POSITION_NUM];
        for (int i = 0; i < FORMATION_POSITION_NUM; i++)
        {
            positionCharaIdArray[i] = DefineParam.CHARA_INVALID;
        }
    }

    public void SetChara(int positionIndex, int charaId)
    {
        positionCharaIdArray[positionIndex] = charaId;
    }

    public int GetCharaId(int positionIndex)
    {
        return positionCharaIdArray[positionIndex];
    }
}
