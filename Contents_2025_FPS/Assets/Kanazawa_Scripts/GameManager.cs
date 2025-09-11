using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static bool isFirstPlay = false;

    private bool isGetWhiteKey = false;
    private bool isGetBlueKey = false;
    private bool isGetRedKey = false;
    private bool isGetGreenKey = false;

    [SerializeField] GameObject reSpawnPos;
    [SerializeField] GameObject player;
    [SerializeField] GameObject volume;
    [SerializeField] KeyManager keyManager;

    private int maxHp;
    private void Start()
    {
        maxHp = player.GetComponent<PlayerController>().GetHp();
    }
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

    public void ReSpawn()
    {
        player.transform.position = reSpawnPos.transform.position;
        player.GetComponent<PlayerController>().TakeDamage(-maxHp);
        player.GetComponent<PlayerController>().SetSpeed(1);
        volume.GetComponent<ColorManager>().ResetColorManager();

        if(keyManager.useRedKey == false)
        {
            isGetRedKey = false;
        }
        if(keyManager.useGreenKey == false)
        {
            isGetBlueKey = false;
        }
        if(keyManager.useBlueKey == false)
        {
            isGetBlueKey = false;
        }
        if(keyManager.useWhiteKey == false)
        {
            isGetWhiteKey = false;
        }
    }

    public void AllReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        Application.Quit();
    }
    public void FirstPlay()
    {
        isFirstPlay = true;
    }
}
