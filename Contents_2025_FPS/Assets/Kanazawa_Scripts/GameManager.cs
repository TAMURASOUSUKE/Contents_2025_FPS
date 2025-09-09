using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private bool isGetWhiteKey = false;
    private bool isGetBlueKey = false;
    private bool isGetRedKey = false;
    private bool isGetGreenKey = false;

    public enum KeyType
    {
        WHITE_KEY,
        BLUE_KEY,
        RED_KEY,
        GREEN_KEY
    }


    public void GetKey(KeyType type)
    {
        switch (type)
        {
            case KeyType.WHITE_KEY:
                isGetWhiteKey = true;
                break;
            case KeyType.BLUE_KEY:
                isGetBlueKey = true;
                break;
            case KeyType.RED_KEY:
                isGetRedKey = true;
                break;
            case KeyType.GREEN_KEY:
                isGetGreenKey = true;
                break;
        }

    }

    public bool GetWhiteKey()
    {
        return isGetWhiteKey;
    }
    public bool GetBlueKey()
    {
        return isGetBlueKey;
    }
    public bool GetRedKey()
    {
        return isGetRedKey;
    }
    public bool GetGreenKey()
    {
        return isGetGreenKey;
    }


}
