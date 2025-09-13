using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] ScenesManagersScripts scenesManagersScripts;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject blad;
    [SerializeField] GameObject gameOverTitle;
    [SerializeField] GameObject respwanObj;
    [SerializeField] GameObject goTitleObj;
    [SerializeField] GameOverButton respawn;
    [SerializeField] GameOverButton goTitle;
    [SerializeField] Sprite[] respawns = new Sprite[2];
    [SerializeField] Sprite[] goTitles = new Sprite[2];
    [SerializeField] Text deadText;

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
            TrapIDManager.TrapID trapID = playerController.GetDeadType();
            switch (trapID)
            {
                case TrapIDManager.TrapID.BearTrap:
                    deadText.text = "死因 : 足首から先が別の人生を歩んだ";
                    break;
                case TrapIDManager.TrapID.PitFall:
                    deadText.text = "死因 : 重力を理解した";
                    break;
                case TrapIDManager.TrapID.Acid:
                    deadText.text = "死因 : 化学を体感した";
                    break;
                case TrapIDManager.TrapID.Color:
                    deadText.text = "死因 : 色に呑まれた";
                    break;
                case TrapIDManager.TrapID.Enemy:
                    deadText.text = "死因 : 閾ｪ霄ｫに食われた";
                    break;
            }




            gameOverTitle.SetActive(true);
            blad.SetActive(true);
            deadText.gameObject.SetActive(true);
            StartCoroutine(FadeIn(null, deadText, 3.5f));
            StartCoroutine(FadeIn(gameOverTitleImg, null, 3f));
            StartCoroutine(FadeIn(respawnImg, null, 4f));
            StartCoroutine(FadeIn(goTitleImg,null, 4f));

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
            deadText.gameObject .SetActive(false);
        }
    }

    IEnumerator FadeIn(Image img = null, Text text = null, float duration = 0)
    {
        if(img == null && text == null) yield break;

        if (img != null && text == null)
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

        if (text != null && img == null)
        {
            Color t = text.color;
            t.a = 0f; // 透明にしてからスタート
            text.color = t;

            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t1 = timer / duration;
                t.a = Mathf.Lerp(0f, 1f, t1);
                text.color = t;
                yield return null; // 次のフレームまで待つ
            }
        }
    }

}
