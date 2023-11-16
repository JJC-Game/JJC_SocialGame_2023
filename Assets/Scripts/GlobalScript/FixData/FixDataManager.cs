using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        fixData_charaFixData = new FixData_CharaFixData();
        fixData_charaFixData.Load();
        fixData_skillFixData = new FixData_SkillFixData();
        fixData_skillFixData.Load(); 
        inGameText_charaName = new InGameText_CharaName();
        inGameText_charaName.Load();
        inGameText_skillName = new InGameText_SkillName();
        inGameText_skillName.Load();

        Debug.Log("CharaName 1 -> " + GetCharaName(1));
        Debug.Log("CharaImagePath 1 -> " + GetCharaImagePath(1));
        Debug.Log("SkillName 1 -> " + GetSkillName(1));
        Debug.Log("SkillEffectPath 1 -> " + GetSkillEffectPath(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FixData_CharaFixData.CharaFixData GetCharaFixData(int charaId)
    {
        return fixData_charaFixData.GetFixData(charaId);
    }
    public FixData_SkillFixData.SkillFixData GetSkillFixData(int skillId)
    {
        return fixData_skillFixData.GetFixData(skillId);
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

    public string GetCharaRarityFrameImage(int charaId)
    {
        switch (GetCharaRarity(charaId))
        {
            case "0":
                return "Textures/RarityFrame/CharaFrame_Common";
            case "1":
                return "Textures/RarityFrame/CharaFrame_Rare";
            case "2":
                return "Textures/RarityFrame/CharaFrame_UltraRare";
            default:
                // charaIdが0、1、2以外の場合のデフォルトの処理をここに追加できます。
                return "Unknown"; // 例: 不明な場合に "Unknown" を返す
        }
    }

    public string GetSkillName(int skillId)
    {
        return inGameText_skillName.GetName(skillId);
    }

    public string GetSkillEffectPath(int skillId)
    {
        return fixData_skillFixData.GetFixData(skillId).effectPath;
    }

    FixData_CharaFixData fixData_charaFixData;
    FixData_SkillFixData fixData_skillFixData;
    InGameText_CharaName inGameText_charaName;
    InGameText_SkillName inGameText_skillName;
}
