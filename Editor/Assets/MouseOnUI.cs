using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static bool mouse_over = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        //Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        //Debug.Log("Mouse exit");
    }

    public bool returnMouseOverUI()
    {
        return mouse_over;
    }
}