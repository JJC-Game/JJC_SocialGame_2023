using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Application.gs2Manager.GetPlayerFormation());
        }
        if (Input.GetMouseButtonDown(1))
        {
            UserDataManager.Formation formation = new UserDataManager.Formation();
            formation.Init();
            formation.SetChara(0, 1);
            formation.SetChara(1, 2);
            formation.SetChara(2, 3);
            StartCoroutine(Application.gs2Manager.SetPlayerFormation(formation));
        }
    }
}
