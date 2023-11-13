using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InGameText_SkillName : MonoBehaviour
{
    public void Load()
    {
        string path = "FixData/InGameText.xlsm_SkillName";

        TextAsset csvFile;
        m_fixDataList = new List<SkillaName>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                SkillaName currentRow = new SkillaName();
                currentRow.id = int.Parse(lineArray[0]);
                currentRow.name = lineArray[1];

                m_fixDataList.Add(currentRow);
            }
        }
    }

    public string GetName(int id)
    {
        if (IsValidId(id))
        {
            return m_fixDataList[id].name;
        }
        return "";
    }

    public SkillaName GetFixData(int id)
    {
        if (IsValidId(id))
        {
            return m_fixDataList[id];
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
            SkillaName data = GetFixData(i);
            Debug.Log(data.id + "," + data.name);
        }
    }

    public class SkillaName
    {
        public int id;
        public string name;
    }

    List<SkillaName> m_fixDataList;
    public const int INVALID_ID = 0;
}
