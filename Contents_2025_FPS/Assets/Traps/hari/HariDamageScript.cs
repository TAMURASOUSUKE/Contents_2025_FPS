using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HariDamageScript : MonoBehaviour
{
    const int DAMAGE = 45;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.TakeDamage(DAMAGE);
            player.SetSpeed(0.5f);
        }
    }
}
