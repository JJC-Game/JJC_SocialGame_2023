using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CommonLineMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isPointing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isPointing = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isPointing = false;
    }

    public bool IsPointing()
    {
        return isPointing;
    }

    public virtual void SetText(string newText)
    {

    }

    public abstract void Click();
}
