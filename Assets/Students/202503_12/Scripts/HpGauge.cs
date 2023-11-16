using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpGauge : MonoBehaviour
{
    Slider S;
   public GameObject characterObjects;
    BattleActor hp;
    // Start is called before the first frame update
    void Start()
    {
        S = this.gameObject.GetComponent<Slider>();
        // 親のスクリプトを取得
        hp = characterObjects.GetComponent<BattleActor>();
    }

    void Update()
    {
        S.maxValue = hp.GetMaxHP();
        // hp.nowが変更されたらゲージの値も更新
        if (S.value != hp.currentHp)
        {
            TakeDamage();
        }
        Debug.Log(hp.currentHp);
    }
    void TakeDamage()
    {
        S.value = hp.currentHp;
    }
}
