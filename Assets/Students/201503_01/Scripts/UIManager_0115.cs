using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_0115 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string charaName = Application.fixDataManager.GetCharaName(1);
        Debug.Log(charaName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
