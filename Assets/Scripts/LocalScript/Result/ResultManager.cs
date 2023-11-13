using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.appSceneManager.IsBattleWin())
        {
            GameObject.Find("BattleResult/Text").GetComponent<Text>().text = "勝利";
        }
        if (Application.appSceneManager.IsBattleLose())
        {
            GameObject.Find("BattleResult/Text").GetComponent<Text>().text = "敗北";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
