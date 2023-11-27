using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DamageHealTextAnimation : MonoBehaviour
{
    public GameObject damage;
    public GameObject heal;
    public TextMeshProUGUI damagetext;
    public TextMeshProUGUI healtext;

    float time;

    int rnd;

    bool countStartDMG;
    bool countStartHL;

    void Update()
    {
        TimeCount();

        if (Input.GetKeyDown(KeyCode.Q) && !countStartDMG && !countStartHL)
        {
            countStartDMG = true;
            rnd = Random.Range(1,9999);
            damagetext.text = rnd.ToString();
            damage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.W) && !countStartDMG && !countStartHL)
        {
            countStartHL = true;
            rnd = Random.Range(1, 9999);
            healtext.text = rnd.ToString();
            heal.SetActive(true);
        }
    }

    void TimeCount()
    {
        if (countStartDMG && !countStartHL)
        {
            time += Time.deltaTime;

            if(time >= 1)
            {
                damage.SetActive(false);
                time = 0;
                countStartDMG = false;
            }
        }
        else if(!countStartDMG && countStartHL)
        {
            time += Time.deltaTime;

            if (time >= 2)
            {
                heal.SetActive(false);
                time = 0;
                countStartHL = false;
            }
        }
    }

    public void CustomEvent()
    {
        Debug.Log("a");
    }
}
