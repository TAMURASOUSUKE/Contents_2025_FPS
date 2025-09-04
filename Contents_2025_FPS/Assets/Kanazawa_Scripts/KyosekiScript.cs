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
    private float currentPosY;
    private float currentPosZ;

    Vector3 kyosekiPos = new Vector3(0,0,0);
    Vector3 startKyosekiPos = new Vector3(1, 4, 22);


    //巨石の移動速度の保存で使用
    private float moveSpeedY;
    private float moveSpeedZ;
    private float currentMoveSpeed;

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
        transform.position = startKyosekiPos;
        previousPosY = currentPosY = KYOSEKISTARTPOSY;
        previousPosZ = currentPosZ = KYOSEKISTARTPOSZ;
    }

    // Update is called once per frame
    void Update()
    {
        kyosekiPos = transform.position;



        previousPosY = currentPosY;
        previousPosZ = currentPosZ;

        currentPosY = kyosekiPos.y;
        currentPosZ = kyosekiPos.z;

        moveSpeedY = currentPosY - previousPosY;
        moveSpeedZ = currentPosZ - previousPosZ;

        currentMoveSpeed = Mathf.Sqrt((moveSpeedY * moveSpeedY) + (moveSpeedZ * moveSpeedZ));

        moveSpeedY = moveSpeedY * (KYOSEKIMAXMOVESPEED / currentMoveSpeed);
        moveSpeedZ = moveSpeedZ * (KYOSEKIMAXMOVESPEED / currentMoveSpeed);










        if (kyosekiPos.y <= KYOSEKIFINISHPOSY && kyosekiPos.z >= KYOSEKIFINISHPOSZ)
        {
            Destroy(gameObject);
        }
    }
}
