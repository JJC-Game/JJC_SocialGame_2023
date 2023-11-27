using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour
{
     Slider S;  // ゲージ
    BattleActor hp;            // HP スクリプトへの参照
   public GameObject characterObjects;      //HP　スクリプトのオブジェクト
     float damageDuration = 1f;  // ダメージが徐々に減少する期間
     bool isRunning = false;
    float startValue;
    float targetValue;
    bool des=false;
    int hpNow;
    int hpMae;
    void Start()
    {
        S = this.gameObject.GetComponent<Slider>();
        hp = characterObjects.GetComponent<BattleActor>();
    }

    void Update()
    {
        S.maxValue = hp.GetMaxHP();
        
        if (S.value != hp.currentHp&&des==false)
        {
            StartCoroutine(TakeDamage());
            targetValue = hp.GetNowHp();
            hpNow = hp.currentHp;
        }
    }

    IEnumerator TakeDamage()
    {
        if (isRunning)
            yield break;
        startValue = hpNow + hp.damageHp;
        float currentTime = 0f;
        isRunning = true;
        if (hp.currentHp <= 0)
        {
            startValue = hpNow;
            des = true;
           
        }

        if(hpMae<= startValue)
        {
            startValue = hpMae;
        }
        while (currentTime < damageDuration)
        {
            float lerpedValue = Mathf.Lerp(startValue, targetValue, currentTime / damageDuration);
            float i = lerpedValue;
            S.value = i;
            currentTime += Time.deltaTime;
            if (Mathf.Abs(S.value - hp.currentHp) < 1)
            {
                S.value = hp.currentHp;
                startValue = hp.currentHp;
                break;
            }

            yield return null;
        }
        isRunning = false;
        hpMae = hp.currentHp;
       
    }
}