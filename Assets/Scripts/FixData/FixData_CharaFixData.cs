using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FixData_CharaFixData
{
    public void Load()
    {
        string path = "FixData/FixData.xlsm_CharaFixData";

        TextAsset csvFile;
        m_fixDataList = new List<CharaFixData>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                CharaFixData currentRow = new CharaFixData();
                currentRow.id = int.Parse(lineArray[0]);
                currentRow.imagePath = lineArray[1];
                currentRow.rarity = int.Parse(lineArray[2]);
                currentRow.hp = int.Parse(lineArray[3]);
                currentRow.physicsAtk = int.Parse(lineArray[4]);
                currentRow.physicsDef = int.Parse(lineArray[5]);
                currentRow.magicAtk = int.Parse(lineArray[6]);
                currentRow.magicDef = int.Parse(lineArray[7]);

                m_fixDataList.Add(currentRow);
            }
        }
    }

    public CharaFixData GetFixData(int charaId)
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
            CharaFixData data = GetFixData(i);
            Debug.Log(data.id + "," + data.imagePath);
        }
    }

    public class CharaFixData
    {
        public int id;
        public string imagePath;
        public int rarity;
        public int hp;
        public int physicsAtk;
        public int physicsDef;
        public int magicAtk;
        public int magicDef;
    }

    List<CharaFixData> m_fixDataList;
    public const int INVALID_ID = 0;
}
