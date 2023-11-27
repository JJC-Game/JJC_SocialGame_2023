using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // 味方の種類、数.
    // 敵の種類、数.
    // バトルの結果.

    Formation allyFormation;
    Formation enemyFormation;

    BattleActor[] battleActorArray;

    // Start is called before the first frame update
    void Start()
    {
        allyFormation = Application.userDataManager.GetFormation();
        SetUpEnemyFormation();

        battleActorArray = new BattleActor[Formation.FORMATION_POSITION_NUM * 2];

        battleActorArray[0] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_0").GetComponent<BattleActor>();
        battleActorArray[1] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_1").GetComponent<BattleActor>();
        battleActorArray[2] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_2").GetComponent<BattleActor>();
        battleActorArray[3] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_0").GetComponent<BattleActor>();
        battleActorArray[4] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_1").GetComponent<BattleActor>();
        battleActorArray[5] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_2").GetComponent<BattleActor>();

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

    private void SetUpEnemyFormation()
    {
        enemyFormation = new Formation();
        enemyFormation.Init();

        enemyFormation.SetChara(0, 201);
        enemyFormation.SetChara(1, 201);
        enemyFormation.SetChara(2, 201);
    }

    public void Attack(BattleActor attacker, FixData_CharaFixData.CharaFixData attackerCharaFixData, FixData_SkillFixData.SkillFixData attackerSkillFixData)
    {
        int targetIndex = GetTargetIndex(0, attacker.GetTeamId());
        BattleActor defender = battleActorArray[targetIndex];

        defender.TakeDamage(attackerCharaFixData, attackerSkillFixData);
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
            Application.appSceneManager.BattleWin();
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
