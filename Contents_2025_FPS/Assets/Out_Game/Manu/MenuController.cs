using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;       // メニュー全体
    [SerializeField] private GameObject ReturnGameButton;       // ゲームに戻るボタン
    [SerializeField] private GameObject ControlManuButton;       // 操作方法ボタン
    [SerializeField] private GameObject ReturnTitleButton;       // タイトルに戻るボタン
    [SerializeField] private VideoPlayer videoPlayer;    // VideoPlayer
    [SerializeField] private RawImage videoDisplay;      // RawImage
    [SerializeField] private RawImage buttonVideoDisplay;      // RawImage

    [SerializeField] private GameObject howToPanel;      // 操作方法画面

    private bool isMenuOpen = false;

    void Update()
    {
        // Tabキーでメニュー表示/非表示
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isMenuOpen = !isMenuOpen;
            menuPanel.SetActive(isMenuOpen);
            PlayVideo();
        }
    }

    // ボタンに割り当てる関数
    public void OnButton1_CloseMenu()
    {
        menuPanel.SetActive(false);
        isMenuOpen = false;
    }

    public void OnButton2_ShowHowTo()
    {
        howToPanel.SetActive(true);
    }

    public void OnButton3_GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void PlayVideo()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }


    public void StopVideo()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }

    // オプション：指定の VideoClip を再生したい場合
    public void PlayClip(VideoClip clip)
    {
        if (videoPlayer == null) return;
        if (clip != null) videoPlayer.clip = clip;
        videoPlayer.Play();
    }
}
