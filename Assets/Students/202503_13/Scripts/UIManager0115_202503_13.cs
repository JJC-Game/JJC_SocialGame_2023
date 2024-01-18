using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager0115_202503_13 : MonoBehaviour
{
    public int CharaID;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.fixDataManager.GetCharaName(CharaID));

        /*Debug.Log(BattleData.BattleFixData,);
        Application.FixDataManager.GetBattleData battleData;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
