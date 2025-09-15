using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class StageHariScript : MonoBehaviour
{
    const int DAMAGE = 30;
    bool isPlayer = false;
    float time = 0.0f;
    float coolTime = 1.0f;
    PlayerController player;

    private void Update()
    {
        if (isPlayer == true)
        {
            //時間立つごとに
            time += Time.deltaTime;
            if (time >= coolTime)
            {
                time = 0.0f;
                player.TakeDamage(DAMAGE);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーが針に当たってきたらプレイヤーがいるフラグを立てます
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            isPlayer = true;
            player.TakeDamage(DAMAGE, TrapIDManager.TrapID.Needle);
            time = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //プレイヤーが離れたらフラグをなくす
        if (other.CompareTag("Player"))
        {
            isPlayer = false;
            time = 0.0f;
        }
    }
}
