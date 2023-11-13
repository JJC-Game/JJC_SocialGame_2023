using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // 味方の種類、数.
    // 敵の種類、数.
    // バトルの結果.

    UserDataManager.Formation allyFormation;
    UserDataManager.Formation enemyFormation;

    BattleActor[] battleActorArray;

    // Start is called before the first frame update
    void Start()
    {
        allyFormation = Application.userDataManager.GetFormation();
        SetUpEnemyFormation();

        battleActorArray = new BattleActor[UserDataManager.FORMATION_NUM * 2];

        battleActorArray[0] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_0").GetComponent<BattleActor>();
        battleActorArray[1] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_1").GetComponent<BattleActor>();
        battleActorArray[2] = GameObject.Find("BattleSpriteAnchor/AllyAnchor/BattleActor_2").GetComponent<BattleActor>();
        battleActorArray[3] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_0").GetComponent<BattleActor>();
        battleActorArray[4] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_1").GetComponent<BattleActor>();
        battleActorArray[5] = GameObject.Find("BattleSpriteAnchor/EnemyAnchor/BattleActor_2").GetComponent<BattleActor>();

        battleActorArray[0].Init(allyFormation.GetCharaId(0));
        battleActorArray[1].Init(allyFormation.GetCharaId(1));
        battleActorArray[2].Init(allyFormation.GetCharaId(2));
        battleActorArray[3].Init(enemyFormation.GetCharaId(0));
        battleActorArray[4].Init(enemyFormation.GetCharaId(1));
        battleActorArray[5].Init(enemyFormation.GetCharaId(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpEnemyFormation()
    {
        enemyFormation = new UserDataManager.Formation();
        enemyFormation.Init();

        enemyFormation.SetChara(0, 201);
        enemyFormation.SetChara(1, 201);
        enemyFormation.SetChara(2, 201);
    }
}
