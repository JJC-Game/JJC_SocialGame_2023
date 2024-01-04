using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BattleData
{
    public void Load()
    {
        string path = "FixData/Battle/BattleData.xlsm_Battle_Output";

        TextAsset csvFile;
        m_fixDataList = new List<BattleFixData>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                BattleFixData currentRow = new BattleFixData();
                currentRow.id = int.Parse(lineArray[0]);
                currentRow.battle1_front = int.Parse(lineArray[1]);
                currentRow.battle1_middle = int.Parse(lineArray[2]);
                currentRow.battle1_back = int.Parse(lineArray[3]);
                currentRow.battle2_front = int.Parse(lineArray[4]);
                currentRow.battle2_middle = int.Parse(lineArray[5]);
                currentRow.battle2_back = int.Parse(lineArray[6]);
                currentRow.battle3_front = int.Parse(lineArray[7]);
                currentRow.battle3_middle = int.Parse(lineArray[8]);
                currentRow.battle3_back = int.Parse(lineArray[9]);
                currentRow.after_battle_scenario_id = int.Parse(lineArray[10]);
                currentRow.obtain_exp = int.Parse(lineArray[11]);
                currentRow.cost_stamina = int.Parse(lineArray[12]);
                currentRow.obtain_item_1_id = int.Parse(lineArray[13]);
                currentRow.obtain_item_1_chance = int.Parse(lineArray[14]);
                currentRow.obtain_item_2_id = int.Parse(lineArray[15]);
                currentRow.obtain_item_2_chance = int.Parse(lineArray[16]);
                currentRow.obtain_item_3_id = int.Parse(lineArray[17]);
                currentRow.obtain_item_3_chance = int.Parse(lineArray[18]);
                 m_fixDataList.Add(currentRow);
            }
        }
    }

    public BattleFixData GetFixData(int battleId)
    {
        if (IsValidId(battleId))
        {
            return m_fixDataList[battleId];
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
            BattleFixData data = GetFixData(i);
            Debug.Log(data.id + "," + data.battle1_front);
        }
    }

    public class BattleFixData
    {
        public int id;
        public int battle1_front;
        public int battle1_middle;
        public int battle1_back;
        public int battle2_front;
        public int battle2_middle;
        public int battle2_back;
        public int battle3_front;
        public int battle3_middle;
        public int battle3_back;
        public int after_battle_scenario_id;
        public int obtain_exp;
        public int cost_stamina;
        public int obtain_item_1_id;
        public int obtain_item_1_chance;
        public int obtain_item_2_id;
        public int obtain_item_2_chance;
        public int obtain_item_3_id;
        public int obtain_item_3_chance;
    }

    List<BattleFixData> m_fixDataList;
    public const int INVALID_ID = 0;
}
