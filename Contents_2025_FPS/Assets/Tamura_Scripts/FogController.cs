using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


// ゴール直前の坂でfogが濃くなるようにする

public class FogController : MonoBehaviour
{
    [SerializeField] Transform player; // プレイヤー
    [SerializeField] Transform startPoint; // fogを操作し始めるところ
    [SerializeField] Transform endPoint; // fogの操作を終えるところ(一番濃い状態)
    [SerializeField] float startDuration = 0.27f; // 最初のfogの強さ
    [SerializeField] float maxDuration = 0.8f; // 霧の最大の濃さ
    [SerializeField] float adj = 0.5f; // 霧の増加を調整する値
    float startValue = 0f; // 計算上の最初の値
    float endValue = 1f; // 計算上の最後の値
    void Start()
    {
        RenderSettings.fogDensity = startDuration; // fogの初期設定
    }

    
    void Update()
    {
        calculatFog();
    }

    void calculatFog()
    {
        // それぞれのz軸で比較する
       float zPlayer = player.position.z;
       float zStart = startPoint.position.z;
       float zEnd = endPoint.position.z;

        //startからendまでどれくらい進んだか
        float t = Mathf.InverseLerp(zStart, zEnd, zPlayer);

        // 値を補完
        float value = Mathf.Lerp(startValue, endValue, t);

        // 実際に代入する
        float totalDuration = (value * adj)  + startDuration;
        if (totalDuration > maxDuration)
        {
            totalDuration = maxDuration; // 最大値を設定
        }
        RenderSettings.fogDensity = totalDuration;
    }
}
