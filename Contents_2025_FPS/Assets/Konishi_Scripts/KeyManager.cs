using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    bool isRedKeyHave = false;
    bool isGreenKeyHave = false;
    bool isBlueKeyHave = false;
    bool isWhiteKeyHave = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsKeyHave();
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRedKeyHave = true;
        }
        if (Input.GetKeyDown(KeyCode.G)) 
        { 
            isGreenKeyHave = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBlueKeyHave = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            isWhiteKeyHave= true;
        }
    }

    void IsKeyHave()
    {
        if (isRedKeyHave)
        {
            animator.SetTrigger("RedKey");
            animator.SetBool("isRedIdle", true);
        }
        if (isGreenKeyHave)
        {
            animator.SetTrigger("GreenKey");
            animator.SetBool("isGreenIdle", true);
        }
        if (isBlueKeyHave)
        {
            animator.SetTrigger("BlueKey");
            animator.SetBool("isBlueIdle", true);
        }
        if (isWhiteKeyHave)
        {
            animator.SetTrigger("WhiteKey");
            animator.SetBool("isWhiteIdle", true);
        }
    }
}
