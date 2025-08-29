using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HariDamageScript : MonoBehaviour
{

    const int damage = 70;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            int nowHp = player.GetHp();

            player.SetHp(nowHp - damage);
        }
    }
}
