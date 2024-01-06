using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRenderer : MonoBehaviour
{
    public int charaId;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshCharaId(int newCharaId)
    {
        charaId = newCharaId;
        string imagePath = Application.fixDataManager.GetCharaImagePath(newCharaId);
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imagePath);
    }
}
