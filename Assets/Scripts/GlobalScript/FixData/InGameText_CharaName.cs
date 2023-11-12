using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InGameText_CharaName
{
    public void Load()
    {
        string path = "FixData/InGameText.xlsm_CharaName";

        TextAsset csvFile;
        m_fixDataList = new List<CharaName>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                CharaName currentRow = new CharaName();
                currentRow.id = int.Parse(lineArray[0]);
                currentRow.name = lineArray[1];

                m_fixDataList.Add(currentRow);
            }
        }
    }

    public string GetCharaName(int charaId)
    {
        if (IsValidId(charaId))
        {
            return m_fixDataList[charaId].name;
        }
        return "";
    }

    public CharaName GetFixData(int charaId)
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
            CharaName data = GetFixData(i);
            Debug.Log(data.id + "," + data.name);
        }
    }

    public class CharaName
    {
        public int id;
        public string name;
    }

    List<CharaName> m_fixDataList;
    public const int INVALID_ID = 0;

}
