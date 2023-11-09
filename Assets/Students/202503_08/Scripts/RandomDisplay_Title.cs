using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomDisplay_Title : MonoBehaviour
{
    public List<Sprite> charaSourceImage = new List<Sprite>();
    Image charaImage;

    Animator anim;
    public float backTime = 10;
    float backEndTime = 1.5f;
    float timer = 0;
    bool backAnimFLG = false;

    void Start()
    {
        backEndTime += backTime;
        charaImage = gameObject.GetComponent<Image>();
        anim = gameObject.GetComponent<Animator>();
        RandomImageSet();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= backTime && !backAnimFLG)
        {
            anim.SetTrigger("Back");
            backAnimFLG = true;
        }
        else if(timer >= backEndTime)
        {
            MoveFinish();
        }
    }

    void RandomImageSet()
    {
        int imageNo = Random.Range(0, charaSourceImage.Count);
        charaImage.sprite = charaSourceImage[imageNo];
    }

    public void MoveFinish()
    {
        timer = 0;
        backAnimFLG = false;
        RandomImageSet();
    }
}
