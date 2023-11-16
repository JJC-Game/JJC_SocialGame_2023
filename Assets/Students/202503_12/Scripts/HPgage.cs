using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPgage : MonoBehaviour
{
    Slider S;
   public GameObject Oya;
    BattleActor hp;
    public float damageSpeed = 10f;
    public Image fillImage;
    // Start is called before the first frame update
    void Start()
    {
        S = this.gameObject.GetComponent<Slider>();
        // 親のスクリプトを取得
        hp = Oya.GetComponent<BattleActor>();
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
