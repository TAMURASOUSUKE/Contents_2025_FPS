using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isHovering { get; private set; } // Getter•Ï”

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log("æ‚Á‚½‚æ");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Debug.Log("o‚½‚æ");
    }
}
