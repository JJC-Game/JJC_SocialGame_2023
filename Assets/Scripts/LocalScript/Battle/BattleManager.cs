using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineParam;

public class BattleManager : MonoBehaviour
{
    // バトル情報.
    int battleId;
    DefineParam.BATTLE_WAVE battleWave;
    BattleData.BattleFixData battleFixData;

    // 味方の種類、数.
    // 敵の種類、数.
    // バトルの結果.

    Formation allyFormation;
    Formation enemyFormation;

    BattleActor[] battleActorArray;

    // Start is called before the first frame update
    void Start()
    {
        battleActorArray = new BattleActor[Formation.FORMATION_POSITION_NUM * 2];

        battleActorArray[0] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_0").GetComponent<BattleActor>();
        battleActorArray[1] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_1").GetComponent<BattleActor>();
        battleActorArray[2] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_2").GetComponent<BattleActor>();
        battleActorArray[3] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_0").GetComponent<BattleActor>();
        battleActorArray[4] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_1").GetComponent<BattleActor>();
        battleActorArray[5] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_2").GetComponent<BattleActor>();

        LoadBattle();
    }

    public void LoadBattle()
    {
        battleId = Application.appSceneManager.GetBattleId();
        battleWave = Application.appSceneManager.GetBattleWave();
        battleFixData = Application.fixDataManager.GetBattleData(battleId);

        allyFormation = Application.userDataManager.GetFormation();
        enemyFormation = new Formation();
        enemyFormation.Init();
        if (battleWave == BATTLE_WAVE.WAVE_1)
        {
            enemyFormation.SetChara(0, battleFixData.battle1_front);
            enemyFormation.SetChara(1, battleFixData.battle1_middle);
            enemyFormation.SetChara(2, battleFixData.battle1_back);
        }else if (battleWave == BATTLE_WAVE.WAVE_2)
        {
            enemyFormation.SetChara(0, battleFixData.battle2_front);
            enemyFormation.SetChara(1, battleFixData.battle2_middle);
            enemyFormation.SetChara(2, battleFixData.battle2_back);
        }
        else if (battleWave == BATTLE_WAVE.WAVE_3)
        {
            enemyFormation.SetChara(0, battleFixData.battle3_front);
            enemyFormation.SetChara(1, battleFixData.battle3_middle);
            enemyFormation.SetChara(2, battleFixData.battle3_back);
        }

        battleActorArray[0].Init(allyFormation.GetCharaId(0), BattleActor.TeamId.Ally);
        battleActorArray[1].Init(allyFormation.GetCharaId(1), BattleActor.TeamId.Ally);
        battleActorArray[2].Init(allyFormation.GetCharaId(2), BattleActor.TeamId.Ally);
        battleActorArray[3].Init(enemyFormation.GetCharaId(0), BattleActor.TeamId.Enemy);
        battleActorArray[4].Init(enemyFormation.GetCharaId(1), BattleActor.TeamId.Enemy);
        battleActorArray[5].Init(enemyFormation.GetCharaId(2), BattleActor.TeamId.Enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attack(BattleActor attacker, FixData_CharaFixData.CharaFixData attackerCharaFixData, FixData_SkillFixData.SkillFixData attackerSkillFixData)
    {
        int targetIndex = GetTargetIndex(0, attacker.GetTeamId());
        BattleActor defender = battleActorArray[targetIndex];

        defender.TakeDamage(attackerCharaFixData, attackerSkillFixData);

        CheckBattleEnd();
    }

    public int GetTargetIndex(int defaultTarget, BattleActor.TeamId attackerTeamId)
    {
        if (attackerTeamId == BattleActor.TeamId.Ally)
        {
            if (!battleActorArray[3].IsDead())
            {
                return 3;
            }
            if (!battleActorArray[4].IsDead())
            {
                return 4;
            }
            if (!battleActorArray[5].IsDead())
            {
                return 5;
            }
        }
        else if (attackerTeamId == BattleActor.TeamId.Enemy)
        {
            if (!battleActorArray[0].IsDead())
            {
                return 0;
            }
            if (!battleActorArray[1].IsDead())
            {
                return 1;
            }
            if (!battleActorArray[2].IsDead())
            {
                return 2;
            }
        }
        return 0;
    }

    public void CheckBattleEnd()
    {
        if (IsAllyWin())
        {
            if (battleWave == BATTLE_WAVE.END_WAVE)
            {
                Application.appSceneManager.BattleWin();
            }
            else
            {
                if (battleWave == BATTLE_WAVE.WAVE_1)
                {
                    Application.appSceneManager.SetBattleWave(BATTLE_WAVE.WAVE_2);
                }else if (battleWave == BATTLE_WAVE.WAVE_2)
                {
                    Application.appSceneManager.SetBattleWave(BATTLE_WAVE.WAVE_3);
                }
                else
                {
                    Debug.Assert(false, "Current Battle Wave" + battleWave.ToString());
                    return;
                }
                LoadBattle();
            }
        }

        if (IsAllyLose())
        {
            Application.appSceneManager.BattleLose();
        }
    }
    public bool IsAllyWin()
    {
        if (battleActorArray[3].IsDead() && battleActorArray[4].IsDead() && battleActorArray[5].IsDead())
        {
            return true;
        }
        return false;
    }
    public bool IsAllyLose()
    {
        if (battleActorArray[0].IsDead() && battleActorArray[1].IsDead() && battleActorArray[2].IsDead())
        {
            return true;
        }
        return false;
    }
}
