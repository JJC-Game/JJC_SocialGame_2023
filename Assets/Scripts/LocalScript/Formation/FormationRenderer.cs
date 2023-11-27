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
    }
    // Start is called before the first frame update
    void Start()
    {
        Refresh(Application.userDataManager.GetFormation());
        SelectPosition(0);
        FindObjectOfType<CharaGridRenderer_ClickCheck>().AddListener(CharaSelect);
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
        int charaId = FindObjectOfType<CharaGridRenderer_ClickCheck>().GetClickedCharaId();
        positionRenderer[currentSelectPositionIndex].SetCharaId(charaId);
    }

    // UI表示用.
    FormationRenderer_Position[] positionRenderer;

    // UI操作用.
    int currentSelectPositionIndex;

}
