using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActor : MonoBehaviour
{

    int charaId;

    FixData_CharaFixData.CharaFixData charaFixData;
    FixData_SkillFixData.SkillFixData skill1FixData;
    FixData_SkillFixData.SkillFixData skill2FixData;

    float skill1Timer;
    float skill2Timer;

    // Start is called before the first frame update
    public void Init(int inputCharaId)
    {
        charaId = inputCharaId;

        charaFixData = Application.fixDataManager.GetCharaFixData(charaId);
        skill1FixData = Application.fixDataManager.GetSkillFixData(charaFixData.skill1Id);
        skill2FixData = Application.fixDataManager.GetSkillFixData(charaFixData.skill2Id);

        skill1Timer = skill1FixData.GetCooldownSec();
        skill2Timer = skill2FixData.GetCooldownSec();
    }

    // Update is called once per frame
    void Update()
    {
        skill1Timer -= Time.deltaTime;
        if (skill1Timer <= 0.0f)
        {
            GameObject effectPrefab = Resources.Load<GameObject>(skill1FixData.effectPath);
            GameObject effectInstance = Instantiate(effectPrefab);
            effectInstance.transform.SetParent(this.transform);
            effectInstance.transform.localPosition = Vector3.zero;

            skill1Timer = skill1FixData.GetCooldownSec();
        }

        skill2Timer -= Time.deltaTime;
        if (skill2Timer <= 0.0f)
        {
            GameObject effectPrefab = Resources.Load<GameObject>(skill2FixData.effectPath);
            GameObject effectInstance = Instantiate(effectPrefab);
            effectInstance.transform.SetParent(this.transform);
            effectInstance.transform.localPosition = Vector3.zero;

            skill2Timer = skill2FixData.GetCooldownSec();
        }
    }
}
