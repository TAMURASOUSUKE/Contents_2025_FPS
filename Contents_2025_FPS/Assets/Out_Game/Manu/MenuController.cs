using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;       // メニュー全体
    [SerializeField] private VideoPlayer videoPlayer;    // VideoPlayer
    [SerializeField] private RawImage videoDisplay;      // RawImage

    [SerializeField] private GameObject howToPanel;      // 操作方法画面

    private bool isMenuOpen = false;

    void Update()
    {
        // Tabキーでメニュー表示/非表示
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isMenuOpen = !isMenuOpen;
            menuPanel.SetActive(isMenuOpen);
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

    // ボタンにカーソルが乗ったときに動画再生
    public void PlayVideo()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    // ボタンからカーソルが外れたら動画停止
    public void StopVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }
}
