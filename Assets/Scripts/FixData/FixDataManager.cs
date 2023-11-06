﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        fixData_charaFixData = new FixData_CharaFixData();
        fixData_charaFixData.Load();              
        inGameText_charaName = new InGameText_CharaName();
        inGameText_charaName.Load();

        Debug.Log("CharaName 1 -> " + GetCharaName(1));
        Debug.Log("CharaImagePath 1 -> " + GetCharaImagePath(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetCharaName(int charaId){
        return inGameText_charaName.GetCharaName(charaId);
    }

    public string GetCharaImagePath(int charaId){
        return fixData_charaFixData.GetFixData(charaId).imagePath;
    }
    
    public string GetCharaRarity(int charaId){
        return fixData_charaFixData.GetFixData(charaId).rarity.ToString();
    }

    public string GetCharaRarityFlameImage(int charaId)
    {
        switch (GetCharaRarity(charaId))
        {
            case "0":
                return "Textures/RarityFrame/CharaFlame_Common";
            case "1":
                return "Textures/RarityFrame/CharaFlame_Rare";
            case "2":
                return "Textures/RarityFrame/CharaFlame_UltraRare";
            default:
                // charaIdが0、1、2以外の場合のデフォルトの処理をここに追加できます。
                return "Unknown"; // 例: 不明な場合に "Unknown" を返す
        }
    }

    FixData_CharaFixData fixData_charaFixData;
    InGameText_CharaName inGameText_charaName;
}
