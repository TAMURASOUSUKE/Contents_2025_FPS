using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torabasami : MonoBehaviour
{
    Animator animator;
    AudioSource audio;
    

    const int DAMAGE = 30;

    bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActive == true)
        {
            if (other.CompareTag("Player"))
            {
                animator.SetTrigger("PlayerEntry");
                audio.Play();

                PlayerController player = other.GetComponent<PlayerController>();
                //ダメージ処理
                player.TakeDamage(DAMAGE, TrapIDManager.TrapID.BearTrap);
                player.SetSpeed(0.2f);

                isActive = false;
            }
        } 
    }

    //アニメーションが終わったら実行する関数
    private void EndAnimation()
    {
        isActive = true;
    }
}
