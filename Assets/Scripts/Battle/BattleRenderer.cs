using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRenderer : MonoBehaviour
{
    public int charaId;

    // Start is called before the first frame update
    void Start()
    {
        string imagePath = Application.fixDataManager.GetCharaImagePath(charaId);
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imagePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
