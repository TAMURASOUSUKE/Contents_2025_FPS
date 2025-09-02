using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOtosiana : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Ž€‚Ê‚Ì‚ÅHP‚Í0‚É‚È‚é
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        player.TakeDamage(player.GetHp());
        player.SetSpeed(0.5f);
    }
}
