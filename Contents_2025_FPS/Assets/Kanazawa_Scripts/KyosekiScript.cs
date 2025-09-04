using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KyosekiScript : MonoBehaviour
{
    Rigidbody kyosekiRigidBody;

    //巨石の位置情報の保存に使用

    Vector3 kyosekiPos = new Vector3(0,0,0);
    Vector3 startKyosekiPos = new Vector3(1, 4, 22);
    Vector3 finishKyosekiPos = new Vector3(1,-15,44);


    //巨石の移動速度の保存で仕様
    private float moveSpeedX;
    private float moveSpeedY;

    //巨石の召喚位置の指定で使用
    float KYOSEKISTARTPOSX = 0.974f;
    float KYOSEKISTARTPOSY = 4.08f;
    float KYOSEKISTARTPOSZ = 21.35f;

    //巨石を消滅させる位置の指定で使用
    float KYOSEKIFINISHPOSX = 1;
    float KYOSEKIFINISHPOSY = -14;
    float KYOSEKIFINISHPOSZ = 43;

    //巨石の最大速度の指定に使用
    public float KYOSEKIMAXMOVESPEED;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = startKyosekiPos;
    }

    // Update is called once per frame
    void Update()
    {
        kyosekiPos = transform.position;

        if(kyosekiPos.y <= KYOSEKIFINISHPOSY && kyosekiPos.z >= KYOSEKIFINISHPOSZ)
        {
            Destroy(gameObject);
        }
        Debug.Log(kyosekiPos.z);

    }
}
