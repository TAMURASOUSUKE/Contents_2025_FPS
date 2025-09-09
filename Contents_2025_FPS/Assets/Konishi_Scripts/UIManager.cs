using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject redEye;
    [SerializeField] GameObject greenEye;
    [SerializeField] GameObject blueEye;
    [SerializeField] GameObject whiteEye;
    [SerializeField] KeyAnim keyAnim;
    bool useRedKey = false;
    bool useGreenKey = false;
    bool useBlueKey = false;
    bool useWhiteKey = false;

    void Start()
    {
        redEye.SetActive(false);
        greenEye.SetActive(false);
        blueEye.SetActive(false);
        whiteEye.SetActive(false);
    }
    void Update()
    {
        Initialize();
        UpdateUI();
    }
    void UpdateUI()
    {
        if (useRedKey)
        {
            redEye.SetActive(true);
        }
        if (useGreenKey)
        {
            greenEye.SetActive(true);
        }
        if (useBlueKey)
        {
            blueEye.SetActive(true);
        }
        if (useWhiteKey)
        {
            whiteEye.SetActive(true);
        }
    }
    void Initialize()
    {
        useRedKey = keyAnim.UseRedKey();
        useGreenKey = keyAnim.UseGreenKey();
        useBlueKey = keyAnim.UseBlueKey();
        useWhiteKey = keyAnim.UseWhiteKey();
    }
}
