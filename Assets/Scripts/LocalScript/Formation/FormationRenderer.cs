using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationRenderer : MonoBehaviour
{
    private void Awake()
    {
        positionRenderer = new FormationRenderer_Position[Formation.FORMATION_POSITION_NUM];

        for (int positionIndex = 0; positionIndex < Formation.FORMATION_POSITION_NUM; positionIndex++)
        {
            string objectName = "UIParts_Position00" + (positionIndex + 1).ToString();
            Debug.Assert(transform.Find(objectName) != null, objectName);
            positionRenderer[positionIndex] = transform.Find(objectName).GetComponent<FormationRenderer_Position>();
            positionRenderer[positionIndex].Init(positionIndex);
        }

        currentSelectPositionIndex = 0;

        charaGridRenderer_formation = FindObjectOfType<CharaGridRenderer_Formation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Refresh(Application.userDataManager.GetFormation());
        SelectPosition(0);
        charaGridRenderer_formation.AddListener(CharaSelect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Refresh(Formation formation)
    {
        for (int positionIndex = 0; positionIndex < Formation.FORMATION_POSITION_NUM; positionIndex++)
        {
            positionRenderer[positionIndex].SetCharaId(formation.GetCharaId(positionIndex));
        }
        
    }

    public void ClearAllPositionSelect()
    {
        for (int positionIndex = 0; positionIndex < Formation.FORMATION_POSITION_NUM; positionIndex++)
        {
            positionRenderer[positionIndex].ClearSelect();
        }
    }

    public void SelectPosition(int positionIndex)
    {
        ClearAllPositionSelect();
        positionRenderer[positionIndex].SetSelectenable(true);
        currentSelectPositionIndex = positionIndex;
    }

    public void CharaSelect()
    {
        // キャラクターの編成状況を更新する.
        // UIを更新.
        int charaId = charaGridRenderer_formation.GetClickedCharaId();
        positionRenderer[currentSelectPositionIndex].SetCharaId(charaId);
        // 編成情報を更新.
        Formation formation = Application.userDataManager.GetFormation();
        formation.SetChara(currentSelectPositionIndex, charaId);


        // チェックマークについて更新.
        charaGridRenderer_formation.ClearCheck();
        for (int positionIndex = 0; positionIndex < Formation.FORMATION_POSITION_NUM; positionIndex++)
        {
            charaGridRenderer_formation.SetCheckEnable(formation.GetCharaId(positionIndex), true);
        }
    }

    // UI表示用.
    FormationRenderer_Position[] positionRenderer;

    // UI操作用.
    int currentSelectPositionIndex;
    CharaGridRenderer_Formation charaGridRenderer_formation;

}
