using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static FixData_CharaFixData;

public class FixData_SkillFixData : MonoBehaviour
{
    public void Load()
    {
        string path = "FixData/FixData.xlsm_SkillFixData";

        TextAsset csvFile;
        m_fixDataList = new List<SkillFixData>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                SkillFixData currentRow = new SkillFixData();
                currentRow.id = int.Parse(lineArray[0]);
                currentRow.effectPath = lineArray[1];
                currentRow.skillAttr = int.Parse(lineArray[2]);
                currentRow.skillType = int.Parse(lineArray[3]);
                currentRow.targetType = int.Parse(lineArray[4]);
                currentRow.skillDamagePer = int.Parse(lineArray[5]);
                currentRow.cooldown = int.Parse(lineArray[6]);

                m_fixDataList.Add(currentRow);
            }
        }
    }

    public SkillFixData GetFixData(int charaId)
    {
        if (IsValidId(charaId))
        {
            return m_fixDataList[charaId];
        }
        return null;
    }

    public int GetFixNum()
    {
        return m_fixDataList.Count;
    }

    public bool IsValidId(int Id)
    {
        return 0 <= Id && Id < GetFixNum();
    }

    public void DB_Disp()
    {
        for (int i = 0; i < GetFixNum(); i++)
        {
            SkillFixData data = GetFixData(i);
            Debug.Log(data.id + "," + data.effectPath);
        }
    }

    public class SkillFixData
    {
        public int id;
        public string effectPath;
        public int skillAttr;
        public int skillType;
        public int targetType;
        public int skillDamagePer;
        public int cooldown;
    }

    List<SkillFixData> m_fixDataList;
    public const int INVALID_ID = 0;
}
