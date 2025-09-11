/*using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class HoverPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuController menuController; // 直接メソッドを呼ぶ
    public VideoClip clip;                // ボタンごとに別動画を割り当てたいときにセット（optional）

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (menuController != null)
        {
            if (clip != null) menuController.PlayClip(clip);
            else menuController.PlayVideo();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (menuController != null) menuController.StopVideo();
    }
}*/