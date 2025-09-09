using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] KeyAnim keyAnim;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (keyAnim.CanDoorOpen())
        {
            animator.SetTrigger("isDoorOpen");
        }
    }
}
