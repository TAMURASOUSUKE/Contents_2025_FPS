using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] GameObject redKey;
    [SerializeField] GameObject greenKey;
    [SerializeField] GameObject blueKey;
    [SerializeField] GameObject whiteKey;
    bool isRedKeyHave = false;
    bool isGreenKeyHave = false;
    bool isBlueKeyHave = false;
    bool isWhiteKeyHave = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
        
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRedKeyHave = true;
            redKey.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGreenKeyHave = true;
            greenKey.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBlueKeyHave = true;
            blueKey.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            isWhiteKeyHave = true;
            whiteKey.SetActive(true);
        }
    }

    public bool GetIsRedKeyHave()
    {
        Debug.Log("returnisRedKey");
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

    public bool CanDoorOpen()
    {
        if (isRedKeyHave && isGreenKeyHave && isBlueKeyHave && isWhiteKeyHave)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
