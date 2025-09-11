using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class ReturnGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private VideoPlayer ReturnGameVideo; // 再生するVideoPlayer
    [SerializeField] private GameObject ReturnGameMovie;      // RawImageなど、表示部分（任意）

    // カーソルがボタンに乗った時
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ReturnGameMovie != null) ReturnGameMovie.SetActive(true);
        if (ReturnGameVideo != null)
        {
            ReturnGameVideo.Play();
        }
    }

    // カーソルが外れた時
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ReturnGameVideo != null)
        {
            ReturnGameVideo.Stop();
        }
        if (ReturnGameMovie != null) ReturnGameMovie.SetActive(false);
    }
}