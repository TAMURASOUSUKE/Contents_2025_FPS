using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KyosekiScript : MonoBehaviour
{
    Rigidbody kyosekiRigidBody;

    //巨石の位置情報の保存に使用
    private float previousPosY;
    private float previousPosZ;
    private float currentPosX;
    private float currentPosY;
    private float currentPosZ;

    Vector3 kyosekiPos = new Vector3(0,0,0);
    Vector3 startKyosekiPos = new Vector3(1, 4, 22);


    //巨石の召喚位置の指定で使用
    float KYOSEKISTARTPOSY = 4;
    float KYOSEKISTARTPOSZ = 22;

    //巨石を消滅させる位置の指定で使用
    float KYOSEKIFINISHPOSY = -14;
    float KYOSEKIFINISHPOSZ = 43;

    //巨石の最大速度の指定に使用
    public float KYOSEKIMAXMOVESPEED;


    // Start is called before the first frame update
    void Start()
    {
        kyosekiRigidBody = GetComponent<Rigidbody>();

        transform.position = startKyosekiPos;
        previousPosY = currentPosY = KYOSEKISTARTPOSY;
        previousPosZ = currentPosZ = KYOSEKISTARTPOSZ;
    }

    // Update is called once per frame
    void Update()
    {
        kyosekiPos = transform.position;

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

    private void OnCollisionEnter(Collision player)
    {
        if(player.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = player.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(150);
        }
    }
}

