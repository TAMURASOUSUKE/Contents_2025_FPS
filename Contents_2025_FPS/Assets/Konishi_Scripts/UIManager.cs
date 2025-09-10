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
    [SerializeField] GameObject titleButton;
    [SerializeField] GameObject respawnButton;

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

    public void TitleUiActive()
    {
        AllUiDeActive();
        titleButton.SetActive(true);
    }
    public void GameUiActive()
    {
        AllUiDeActive();
        keyFrame.SetActive(true);
    }
    public void GameOverActive()
    {
        AllUiDeActive();
        respawnButton.SetActive(true);
    }

    private void AllUiDeActive()
    {
        keyFrame.SetActive(false);
        redEye.SetActive(false);
        greenEye.SetActive(false);
        blueEye.SetActive(false);
        whiteEye.SetActive(false);
        titleButton.SetActive(false);
        respawnButton.SetActive(false);
    }
}
