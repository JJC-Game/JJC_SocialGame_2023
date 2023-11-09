using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomDisplay : MonoBehaviour
{
    public List<Sprite> charaSourceImage = new List<Sprite>();
    Image charaImage;

    void Start()
    {
        charaImage = gameObject.GetComponent<Image>();
        int imageNo = Random.Range(0, charaSourceImage.Count);
        charaImage.sprite = charaSourceImage[imageNo];
    }
}
