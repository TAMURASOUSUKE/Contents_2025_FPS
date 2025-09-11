using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class ControlManuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private VideoPlayer ControlManuVideo; // 再生するVideoPlayer
    [SerializeField] private GameObject ControlManuMovie;      // RawImageなど、表示部分（任意）

    // カーソルがボタンに乗った時
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ControlManuMovie != null) ControlManuMovie.SetActive(true);
        if (ControlManuVideo != null)
        {
            ControlManuVideo.Play();
        }
    }

    // カーソルが外れた時
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ControlManuVideo != null)
        {
            ControlManuVideo.Stop();
        }
        if (ControlManuMovie != null) ControlManuMovie.SetActive(false);
    }
}