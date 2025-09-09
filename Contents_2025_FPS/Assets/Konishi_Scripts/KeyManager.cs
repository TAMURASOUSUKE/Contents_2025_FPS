using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] GameObject redKey;
    [SerializeField] GameObject greenKey;
    [SerializeField] GameObject blueKey;
    [SerializeField] GameObject whiteKey;
    [SerializeField] GameManager gameManager;
    [SerializeField] KeyAnim keyAnim;
    bool isRedKeyHave;
    bool isGreenKeyHave;
    bool isBlueKeyHave;
    bool isWhiteKeyHave;
    bool useRedKey = false;
    bool useGreenKey = false;
    bool useBlueKey = false;
    bool useWhiteKey = false;

    void Update()
    {
        Initialoze();
        KeyActive();
    }
    void KeyActive()
    {
        if (useRedKey)
        {
            redKey.SetActive(true);
        }
        if (useGreenKey)
        {
            greenKey.SetActive(true);
        }
        if (useBlueKey)
        {
            blueKey.SetActive(true);
        }
        if (useWhiteKey)
        {
            whiteKey.SetActive(true);
        }
    }

    void Initialoze()
    {
        isRedKeyHave = gameManager.GetRedKey();
        isGreenKeyHave = gameManager.GetGreenKey();
        isBlueKeyHave = gameManager.GetBlueKey();
        isWhiteKeyHave = gameManager.GetWhiteKey();
        useRedKey = keyAnim.UseRedKey();
        useGreenKey = keyAnim.UseGreenKey();
        useBlueKey = keyAnim.UseBlueKey();
        useWhiteKey = keyAnim.UseWhiteKey();
    }

    public bool GetIsRedKeyHave()
    {
        return isRedKeyHave;
    }
    public bool GetIsGreenKeyHave()
    {
        return isGreenKeyHave;
    }
    public bool GetIsBlueKeyHave()
    {
        return isBlueKeyHave;
    }
    public bool GetIsWhiteKeyHave()
    {
        return isWhiteKeyHave;
    }
}
