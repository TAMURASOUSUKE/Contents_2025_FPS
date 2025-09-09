using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] KeyManager keyManager;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (keyManager.CanDoorOpen())
        {
            animator.SetTrigger("isDoorOpen");
        }
    }
}
