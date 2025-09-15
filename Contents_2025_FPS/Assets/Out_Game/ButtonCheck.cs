using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isHovering { get; private set; }

    public void OnPointerEnter(PointerEventData e) => isHovering = true;
    public void OnPointerExit(PointerEventData e) => isHovering = false;

    void OnDisable() => isHovering = false;   // š‚±‚ê‘åŽ–
    public void ForceExit() => isHovering = false; // –¾Ž¦“I‚É—Ž‚Æ‚µ‚½‚¢Žž—p
}
