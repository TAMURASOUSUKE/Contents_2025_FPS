using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyosekiGenerator : MonoBehaviour
{
    //プレファブの取得と生成速度の設定
    public GameObject KyosekiPrefab;
    public float span1 = 0.6f;
    public float delta = 1.2f;

    void Update()
    {
        //deltaがspanを超えたとき巨石を生成
        this.delta += Time.deltaTime;
        if (this.delta > this.span1)
        {
            GameObject go = Instantiate(KyosekiPrefab);

            go.transform.position = new Vector3(1,4,22);
            this.delta = 0;
        }
    }
}
