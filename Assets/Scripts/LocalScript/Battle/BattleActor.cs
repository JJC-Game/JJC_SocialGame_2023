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

    private enum FlagId
    {
        IsDead = 0,
        Num
    }
    bool[] flagArray;

    VariableParam hp;
    public int currentHp;

    public enum TeamId
    {
        Ally = 0,
        Enemy = 1
    }

    TeamId teamId;

    // Start is called before the first frame update
    public void Init(int inputCharaId, TeamId inputTeamId)
    {
        charaId = inputCharaId;

        charaFixData = Application.fixDataManager.GetCharaFixData(charaId);
        skill1FixData = Application.fixDataManager.GetSkillFixData(charaFixData.skill1Id);
        skill2FixData = Application.fixDataManager.GetSkillFixData(charaFixData.skill2Id);

        skill1Timer = skill1FixData.GetCooldownSec();
        skill2Timer = skill2FixData.GetCooldownSec();

        flagArray = new bool[(int)FlagId.Num];
        for (int i = 0; i < (int)FlagId.Num; i++)
        {
            flagArray[i] = false;
        }

        hp = new VariableParam();
        hp.SetMaxValue(charaFixData.hp);
        hp.SetFullValue();

        teamId = inputTeamId;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            return;
        }

        skill1Timer -= Time.deltaTime;
        if (skill1Timer <= 0.0f)
        {
            GameObject effectPrefab = Resources.Load<GameObject>(skill1FixData.effectPath);
            GameObject effectInstance = Instantiate(effectPrefab);
            effectInstance.transform.SetParent(this.transform);
            effectInstance.transform.localPosition = Vector3.zero;

            skill1Timer = skill1FixData.GetCooldownSec();

            GameObject.Find("BattleManager").GetComponent<BattleManager>().Attack(this, charaFixData, skill1FixData);
        }

        skill2Timer -= Time.deltaTime;
        if (skill2Timer <= 0.0f)
        {
            GameObject effectPrefab = Resources.Load<GameObject>(skill2FixData.effectPath);
            GameObject effectInstance = Instantiate(effectPrefab);
            effectInstance.transform.SetParent(this.transform);
            effectInstance.transform.localPosition = Vector3.zero;

            skill2Timer = skill2FixData.GetCooldownSec();

            GameObject.Find("BattleManager").GetComponent<BattleManager>().Attack(this, charaFixData, skill2FixData);
        }

        currentHp = hp.GetNowValue();
    }

    public void TakeDamage(FixData_CharaFixData.CharaFixData attackerCharaFixData, FixData_SkillFixData.SkillFixData attackerSkillFixData)
    {
        int damage = (int)(attackerCharaFixData.physicsAtk * (attackerSkillFixData.skillDamagePer / 100.0f));
        hp.AddNowValue(-damage);

        if (hp.IsEmpty())
        {
            flagArray[(int)FlagId.IsDead] = true;
        }
        currentHp = hp.GetNowValue();
    }

    public bool IsDead()
    {
        return flagArray[(int)FlagId.IsDead];
    }

    public TeamId GetTeamId()
    {
        return teamId;
    }
}
