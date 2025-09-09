using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnim : MonoBehaviour
{
    [SerializeField] KeyManager keyManager;
    Animator animator;

    bool isRedKeyHave;
    bool isGreenKeyHave;
    bool isBlueKeyHave;
    bool isWhiteKeyHave;
    bool useRedKey = false;
    bool useGreenKey = false;
    bool useBlueKey = false;
    bool useWhiteKey = false;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        InputKey();
        IsKeyHave();
        Initialize();
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isRedKeyHave)
            {
                animator.SetTrigger("RedKey");
                useRedKey = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            if (isGreenKeyHave)
            {
                Debug.Log("hokora");
                animator.SetTrigger("GreenKey");
                useGreenKey = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isBlueKeyHave)
            {
                animator.SetTrigger("BlueKey");
                useBlueKey = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isWhiteKeyHave)
            {
                animator.SetTrigger("WhiteKey");
                useWhiteKey = true;
            }
        }
    }

    void IsKeyHave()
    {//InputKey‚Ì“à—e‚ðˆÚ‚·‚Æ‚±‚ë
        
    }
    void Initialize()
    {
        isRedKeyHave = keyManager.GetIsRedKeyHave();
        isGreenKeyHave = keyManager.GetIsGreenKeyHave();
        isBlueKeyHave = keyManager.GetIsBlueKeyHave();
        isWhiteKeyHave = keyManager.GetIsWhiteKeyHave();
    }
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
    public bool UseRedKey()
    {
        return useRedKey;
    }
    public bool UseGreenKey()
    {
        return useGreenKey;
    }
    public bool UseBlueKey()
    {
        return useBlueKey;
    }
    public bool UseWhiteKey()
    {
        return useWhiteKey;
    }
}
