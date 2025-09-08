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
            hari.GetComponent<MeshRenderer>().enabled = true;
            hari.GetComponent<BoxCollider>().enabled = true;
            animator.SetTrigger("PlayerEntry");
        }
    }

    private void OnEndAnimation()
    {
        hari.GetComponent<MeshRenderer>().enabled = false;
        hari.GetComponent<BoxCollider>().enabled = false;
    }
}
