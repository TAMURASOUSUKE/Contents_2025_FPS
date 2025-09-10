using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyManager : MonoBehaviour
{
    public bool useRedKey = false;
    public bool useGreenKey = false;
    public bool useBlueKey = false;
    public bool useWhiteKey = false;

    public bool CanDoorOpen()
    {
        if (useRedKey && useGreenKey && useBlueKey && useWhiteKey)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
