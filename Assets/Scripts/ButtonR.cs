using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonR : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static ButtonR ButR;
    bool IsPress;

    private void Awake() {
        ButR = this;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        IsPress = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       IsPress = false;
    }

    public bool IsPressButtonR()
    {
        return IsPress;
    }
}
