using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class ReturnTitleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private VideoPlayer ReturnTitleVideo; // 再生するVideoPlayer
    [SerializeField] private GameObject ReturnTitleMovie;      // RawImageなど、表示部分（任意）
    [SerializeField] private GameObject returnTitleButton;
    // カーソルがボタンに乗った時
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ReturnTitleMovie != null) ReturnTitleMovie.SetActive(true);
        if (ReturnTitleVideo != null)
        {
            ReturnTitleVideo.Play();
        }
    }

    // カーソルが外れた時
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ReturnTitleVideo != null)
        {
            ReturnTitleVideo.Stop();
        }
        if (ReturnTitleMovie != null) ReturnTitleMovie.SetActive(false);
    }
}