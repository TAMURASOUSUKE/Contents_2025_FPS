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


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        IsKeyHave();
        isRedKeyHave = keyManager.GetIsRedKeyHave();
        isGreenKeyHave = keyManager.GetIsGreenKeyHave();
        isBlueKeyHave = keyManager.GetIsBlueKeyHave();
        isWhiteKeyHave = keyManager.GetIsWhiteKeyHave();
    }

    void IsKeyHave()
    {
        if (isRedKeyHave)
        {
            Debug.Log("anim");
            animator.SetTrigger("RedKey");
        }
        if (isGreenKeyHave)
        {
            animator.SetTrigger("GreenKey");
        }
        if (isBlueKeyHave)
        {
            animator.SetTrigger("BlueKey");
        }
        if (isWhiteKeyHave)
        {
            animator.SetTrigger("WhiteKey");
        }
    }
}
