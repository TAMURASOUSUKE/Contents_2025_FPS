using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtosianaScripts : MonoBehaviour
{
    Animator animator;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerEntry");
            audio.Play();
        }
    }
}
