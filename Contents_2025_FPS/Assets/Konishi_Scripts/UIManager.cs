using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject keyFrame;
    [SerializeField] GameObject redEye;
    [SerializeField] GameObject greenEye;
    [SerializeField] GameObject blueEye;
    [SerializeField] GameObject whiteEye;
    [SerializeField] GameObject button;

    [SerializeField] KeyManager keyManager;
    void Start()
    {
        keyFrame.SetActive(false);
        redEye.SetActive(false);
        greenEye.SetActive(false);
        blueEye.SetActive(false);
        whiteEye.SetActive(false);
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
        if (keyManager.useGreenKey)
        {
            greenEye.SetActive(true);
        }
        if (keyManager.useBlueKey)
        {
            blueEye.SetActive(true);
        }
        if (keyManager.useWhiteKey)
        {
            whiteEye.SetActive(true);
        }
    }

    public void TitleUiSetActive(bool setBool)
    {
        AllUiDeActive();
        button.SetActive(setBool);
    }
    public void GameUiSetActive(bool setBool)
    {
        AllUiDeActive();
        keyFrame.SetActive(setBool);
    }

    private void AllUiDeActive()
    {
        keyFrame.SetActive(false);
        redEye.SetActive(false);
        greenEye.SetActive(false);
        blueEye.SetActive(false);
        whiteEye.SetActive(false);
        button.SetActive(false);
    }
}
