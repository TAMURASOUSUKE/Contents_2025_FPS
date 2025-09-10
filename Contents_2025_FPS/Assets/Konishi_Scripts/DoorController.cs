using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour,IInteractObject
{
    [SerializeField] KeyManager keyManager;
    Animator animator;

    bool isInteract = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnTriggerInteract()
    {
        isInteract = true;
        animator.SetTrigger("isDoorOpen");
    }
    public bool GetCanInteract()
    {
        Debug.Log("doa");
        if(isInteract == false)
        {
            if (keyManager.CanDoorOpen())
            {
                return true;
            }
        }

        return false;
    }
}
