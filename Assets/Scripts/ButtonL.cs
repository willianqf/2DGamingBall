using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonL : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static ButtonL ButL;
    bool IsPress;
    public void OnPointerDown(PointerEventData eventData)
    {
        IsPress = true;
    }
    private void Awake() {
        ButL = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       IsPress = false;
    }

    public bool IsPressButtonL()
    {
        return IsPress;
    }
}
