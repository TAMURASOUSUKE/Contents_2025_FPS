using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KyosekiScript : MonoBehaviour
{

    Rigidbody kyosekiRigidBody;

    //巨石の位置情報の保存に使用
    Vector3 kyosekiPos = new Vector3(0,0,0);

    //巨石を消滅させる位置の指定で使用
    float KYOSEKIFINISHPOSY = -13;
    float KYOSEKIFINISHPOSZ = 42;

    //巨石の最大速度の指定に使用
    public float KYOSEKIMAXMOVESPEED;


    // Start is called before the first frame update
    void Start()
    {
        kyosekiRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //座標の取得
        kyosekiPos = transform.position;
        //削除する位置に来たらオブジェクトを削除
        if (kyosekiPos.y <= KYOSEKIFINISHPOSY && kyosekiPos.z >= KYOSEKIFINISHPOSZ)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        // 現在の速度
        Vector3 kyosekiSpeed = kyosekiRigidBody.velocity;

        // 上限を超えたらClampで制限をかける
        if (kyosekiSpeed.magnitude > KYOSEKIMAXMOVESPEED)
        {
            kyosekiRigidBody.velocity = kyosekiSpeed.normalized * KYOSEKIMAXMOVESPEED;
        }
    }
    //プレイヤーに触れたとき150ダメージを与える
    private void OnCollisionEnter(Collision player)
    {
        if(player.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = player.gameObject.GetComponent<PlayerController>();
            int damage = playerController.GetHp();
            playerController.TakeDamage(damage);
        }
    }
}

