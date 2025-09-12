using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] ScenesManagersScripts scenesManagersScripts;
    [SerializeField] GameObject blad;
    [SerializeField] GameObject gameOverTitle;
    [SerializeField] GameObject respwanObj;
    [SerializeField] GameObject goTitleObj;
    [SerializeField] GameOverButton respawn;
    [SerializeField] GameOverButton goTitle;
    [SerializeField] Sprite[] respawns = new Sprite[2];
    [SerializeField] Sprite[] goTitles = new Sprite[2];

    Image gameOverTitleImg;
    Image respawnImg;
    Image goTitleImg;
    void Start()
    {
        gameOverTitle.SetActive(false);
        blad.SetActive(false);
        gameOverTitleImg = gameOverTitle.GetComponent<Image>();
        respawnImg = respawn.GetComponent<Image>();
        goTitleImg = goTitle.GetComponent<Image>();
        respawnImg.sprite = respawns[0];
        goTitleImg.sprite = goTitles[0];
    }
    void Update()
    {
        ChangeUI();
    }

    void ChangeUI()
    {
        if(scenesManagersScripts.currentScene == ScenesManagersScripts.Scene.GAMEOVER)
        {
            gameOverTitle.SetActive(true);
            blad.SetActive(true);
            StartCoroutine(FadeIn(gameOverTitleImg, 3f));
            StartCoroutine(FadeIn(respawnImg, 4f));
            StartCoroutine(FadeIn(goTitleImg, 4f));

            if (respawn.isHovering)
            {
                respawnImg.sprite = respawns[1];
            }
            else
            {
                respawnImg.sprite = respawns[0];
            }

            if (goTitle.isHovering)
            {
                goTitleImg.sprite = goTitles[1];
            }
            else
            {
                goTitleImg.sprite = goTitles[0];
            }
        }
        else
        {
            gameOverTitle.SetActive(false);
            blad.SetActive(false);
        }
    }

    IEnumerator FadeIn(Image img, float duration)
    {
        Color c = img.color;
        c.a = 0f; // 透明にしてからスタート
        img.color = c;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            c.a = Mathf.Lerp(0f, 1f, t);
            img.color = c;
            yield return null; // 次のフレームまで待つ
        }

        c.a = 1f;
        img.color = c;
    }

}
