using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torabasami : MonoBehaviour
{
    Animator animator;

    const int DAMAGE = 60;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerEntry");

            PlayerController player = other.GetComponent<PlayerController>();
            Damage(player);
        }
    }

    //É_ÉÅÅ[ÉW
    void Damage(PlayerController player)
    {
        int nowHp = player.GetHp();

        player.SetHp(nowHp - DAMAGE);
    }
}
