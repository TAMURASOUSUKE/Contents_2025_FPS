using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] firstTitleSceneUi;
    [SerializeField] GameObject[] titleSceneUi;
    [SerializeField] GameObject[] gameSceneUi;
    [SerializeField] GameObject[] gameOverSceneUi;
    [SerializeField] GameObject[] clearSceneUi;
    [SerializeField] GameObject redEye;
    [SerializeField] GameObject greenEye;
    [SerializeField] GameObject blueEye;
    [SerializeField] GameObject whiteEye;

    [SerializeField] KeyManager keyManager;
    void Start()
    {
        if (GameManager.isFirstPlay == false)
        {
            FirstTitleUiActive();
        }
        else
        {
            GameUiActive();
        }
    }
    void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        if (keyManager.useRedKey)
        {
            redEye.SetActive(true);
        }
        else
        {
            redEye.SetActive(false);
        }
        if (keyManager.useGreenKey)
        {
            greenEye.SetActive(true);
        }
        else
        {
            greenEye.SetActive(false);
        }
        if (keyManager.useBlueKey)
        {
            blueEye.SetActive(true);
        }
        else
        {
            blueEye.SetActive(false);
        }
        if (keyManager.useWhiteKey)
        {
            whiteEye.SetActive(true);
        }
        else
        {
            whiteEye.SetActive(false);
        }
    }

    private void FirstTitleUiActive()
    {
        AllUiDeActive();
        foreach(GameObject ui in firstTitleSceneUi)
        {
            ui.SetActive(true);
        }
    }
    
    public void TitleUiActive()
    {
        AllUiDeActive();
        foreach(GameObject ui in titleSceneUi)
        {
            ui.SetActive(true);
        }
    }
    public void GameUiActive()
    {
        AllUiDeActive();
        foreach (GameObject ui in gameSceneUi)
        {
            ui.SetActive(true);
        }
    }
    public void GameOverActive()
    {
        AllUiDeActive();
        foreach (GameObject ui in gameOverSceneUi)
        {
            ui.SetActive(true);
        }
    }

    public void ClearSceneActive()
    {
        AllUiDeActive();
        foreach (GameObject ui in clearSceneUi)
        {
            ui.SetActive(true);
        }
    }

    private void AllUiDeActive()
    {
        foreach (GameObject ui in firstTitleSceneUi)
        {
            ui.SetActive(false);
        }
        foreach (GameObject ui in titleSceneUi)
        {
            ui.SetActive(false);
        }
        foreach (GameObject ui in gameSceneUi)
        {
            ui.SetActive(false);
        }
        foreach (GameObject ui in gameOverSceneUi)
        {
            ui.SetActive(false);
        }
        foreach (GameObject ui in clearSceneUi)
        {
            ui.SetActive(false);
        }
    }
}
