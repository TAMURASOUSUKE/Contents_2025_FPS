using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Animation : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;       // メニュー全体
    [SerializeField] private VideoPlayer videoPlayer;    // VideoPlayer
    [SerializeField] private RawImage videoDisplay;      // RawImage

    private bool isMenuOpen = false;
    void Update()
    {
        // Tabキーでメニュー表示/非表示
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;
            menuPanel.SetActive(isMenuOpen);
            if (isMenuOpen)
            {
                videoPlayer.Play();
            }
            if (isMenuOpen == false)
            {
                videoPlayer.Stop();
            }
        }
    }

}
