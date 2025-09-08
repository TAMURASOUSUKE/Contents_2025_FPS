using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HariScript : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    GameObject hari;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hari.SetActive(true);
            animator.SetTrigger("PlayerEntry");
        }
    }

    private void OnEndAnimation()
    {
        hari.SetActive(false);
    }
}
