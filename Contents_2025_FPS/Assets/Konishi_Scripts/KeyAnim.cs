using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KeyAnim : MonoBehaviour, IInteractObject
{
    [SerializeField] GameManager.KeyType keyType;

    [SerializeField] GameObject key;

    KeyManager keyManager;

    [SerializeField] GameManager gameManager;
   
    Animator animator;

    bool isRedKeyHave;
    bool isGreenKeyHave;
    bool isBlueKeyHave;
    bool isWhiteKeyHave;
   
    bool isInteract = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        keyManager = GetComponentInParent<KeyManager>();
    }

    private void Update()
    {
        Initialize();
    }

    public void OnTriggerInteract()
    {
        isInteract = true;
        key.SetActive(true);
        switch(keyType)
        {
            case GameManager.KeyType.RED_KEY:
                animator.SetTrigger("RedKey");
                keyManager.useRedKey = true;
                break;
            case GameManager.KeyType.BLUE_KEY:
                animator.SetTrigger("BlueKey");
                keyManager.useBlueKey = true;
                break;

            case GameManager.KeyType.GREEN_KEY:
                animator.SetTrigger("GreenKey");
                keyManager.useGreenKey = true;
                break;
            case GameManager.KeyType.WHITE_KEY:
                animator.SetTrigger("WhiteKey");
                keyManager.useWhiteKey = true;
                break;
        }
    }

    public bool GetCanInteract()
    {
        if (isInteract == false)
        {
            switch (keyType)
            {
                case GameManager.KeyType.RED_KEY:
                    if (isRedKeyHave)
                    {
                        return true;
                    }
                    break;
                case GameManager.KeyType.BLUE_KEY:
                    if (isBlueKeyHave)
                    {
                        return true;
                    }
                    break;

                case GameManager.KeyType.GREEN_KEY:
                    if (isGreenKeyHave)
                    {
                        return true;
                    }
                    break;
                case GameManager.KeyType.WHITE_KEY:
                    if (isWhiteKeyHave)
                    {
                        return true;
                    }
                    break;
            }
        }

        return false;
    }

    void Initialize()
    {
        isRedKeyHave = gameManager.GetRedKey();
        isGreenKeyHave = gameManager.GetGreenKey();
        isBlueKeyHave = gameManager.GetBlueKey();
        isWhiteKeyHave = gameManager.GetWhiteKey();
    }
}
