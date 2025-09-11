using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class ReturnGameButton : MonoBehaviour, IPointerClickHandler//IPointerExitHandler
{
    [SerializeField] private VideoPlayer returnGameVideo; // 再生するVideoPlayer
    [SerializeField] private GameObject returnGameMovie;      // RawImageなど、表示部分
    [SerializeField] private GameObject returnGameButton;

    private bool isMenuOpen = false;

    // カーソルがボタンに乗った時
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("tesuto");
        /*returnGameButton.SetActive(true);
        if (returnGameMovie != null) returnGameMovie.SetActive(true);
        if (returnGameVideo != null)
        {
            returnGameVideo.Play();
        }*/
    }

    // カーソルが外れた時
    /*public void OnPointerExit(PointerEventData eventData)
    {
        if (returnGameVideo != null)
        {
            returnGameVideo.Stop();
        }
        if (returnGameMovie != null) returnGameMovie.SetActive(false);
    }*/
}