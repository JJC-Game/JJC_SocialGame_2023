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
    
    void Start()
    {
        S = this.gameObject.GetComponent<Slider>();
        hp = characterObjects.GetComponent<BattleActor>();
    }

    void Update()
    {
        S.maxValue = hp.GetMaxHP();
        
        if (S.value != hp.currentHp)
        {
            StartCoroutine(TakeDamage());
            targetValue = hp.GetNowHp();
        }
    }

    IEnumerator TakeDamage()
    {
        if (isRunning)
            yield break;
        startValue = hp.GetNowHp() + hp.damageHp;
        
        float currentTime = 0f;
        isRunning = true;
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
    }
}