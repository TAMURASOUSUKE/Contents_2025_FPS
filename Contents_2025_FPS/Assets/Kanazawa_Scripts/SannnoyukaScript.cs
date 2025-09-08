using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SannnoyukaScript : MonoBehaviour
{
    bool isPlayer = false;
    [SerializeField] private int DAMAGE = 10;
    [SerializeField] private float span;
    public float delta;

    PlayerController player;

    private void Update()
    {
        if (isPlayer == true)
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.span)
            {
                player.TakeDamage(DAMAGE);

                this.delta = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            player.TakeDamage(DAMAGE);
            delta = 0.0f;
            isPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            delta = 0.0f;
            isPlayer = false;
        }
    }
}
