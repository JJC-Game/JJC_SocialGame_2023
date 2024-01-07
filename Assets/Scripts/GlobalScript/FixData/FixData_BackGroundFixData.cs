using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FixData_BackGroundFixData
{
    public void Load()
    {
        string path = "FixData/FixData.xlsm_BackGroundFixData";

        TextAsset csvFile;
        m_fixDataList = new List<BackGroundFixData>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                BackGroundFixData currentRow = new BackGroundFixData();
                currentRow.id = int.Parse(lineArray[0]);
                currentRow.imagePath = lineArray[1];
                currentRow.yOffset = (float)(int.Parse(lineArray[2]) / 100.0f); ;

                m_fixDataList.Add(currentRow);
            }
        }
    }

    public BackGroundFixData GetFixData(int charaId)
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
            BackGroundFixData data = GetFixData(i);
            Debug.Log(data.id + "," + data.imagePath);
        }
    }

    public class BackGroundFixData
    {
        public int id;
        public string imagePath;
        public float yOffset;
    }

    List<BackGroundFixData> m_fixDataList;
    public const int INVALID_ID = 0;
}
