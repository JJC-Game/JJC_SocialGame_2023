using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{
    public float duration = 5.0F;   //色が変わるタイミング(時間)
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    [System.Obsolete]

    void Update()
    {
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amplitube = Mathf.Cos(phi) * 0.5F + 0.5F;
        ps.startColor = Color.HSVToRGB(amplitube, 1, 1);
    }
}
