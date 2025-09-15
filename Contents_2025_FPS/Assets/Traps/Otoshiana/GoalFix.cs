using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFix : MonoBehaviour
{
    [SerializeField] PlayerController controller;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int currentHP = controller.GetHp();
            controller.TakeDamage(currentHP, TrapIDManager.TrapID.Rock);
        }
    }
}
