using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


// ゴール直前の坂でfogが濃くなるようにする
// ゴールする直前でfogが白くなる

public class FogController : MonoBehaviour
{
    // ---------------------------------------------------変数-------------------------------------------------

    [SerializeField] Transform player; // プレイヤー
    [SerializeField] Transform startPoint; // fogを操作し始めるところ
    [SerializeField] Transform endPoint; // fogの操作を終えるところ(一番濃い状態)
    [SerializeField] Transform goalStart; // ゴールするとき色を変え始める位置
    [SerializeField] Transform goalEnd; // ゴールするとき色を変え終える位置
    [SerializeField] Color startColor; // 最初のfogの色
    [SerializeField] Color endFogColor = Color.white; // fogのゴールするときの色
    [SerializeField] float startDuration = 0.27f; // 最初のfogの強さ
    [SerializeField] float maxDuration = 0.8f; // 霧の最大の濃さ
    [SerializeField] float adj = 0.5f; // 霧の増加を調整する値
    
    float startValue = 0f; // 計算上の最初の値
    float endValue = 1f; // 計算上の最後の値

    // ----------------------------------------------------関数------------------------------------------------

    void Start()
    {
        RenderSettings.fogDensity = startDuration; // fogの初期設定
        RenderSettings.fogColor = startColor; // fogの色の初期設定
    }

   
    
    void Update()
    {
        CalculatDensity();
        CalculatColor();
    }

    void CalculatDensity()
    {
        // それぞれのz軸で比較する
       float zPlayer = player.position.z;
       float zStart = startPoint.position.z;
       float zEnd = endPoint.position.z;
       

        // 濃さの調整

        //startからendまでどれくらい進んだか
        float t = Mathf.InverseLerp(zStart, zEnd, zPlayer); // fogの濃さを切り替えるとき用

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

    void CalculatColor()
    {
        // ゴール前の坂を下った後でしか計算しないようにする
        if (player.position.z > endPoint.position.z)
        {
            // それぞれのx軸で比較
            float xPlayer = player.position.x;
            float xGoalStart = goalStart.position.x;
            float xGoalEnd = goalEnd.position.x;


            // fogの色を切り替えるとき用
            float tColor = Mathf.InverseLerp(xGoalStart, xGoalEnd, xPlayer);

            // 値の補完
            Color valueColor = Color.Lerp(startColor, endFogColor, tColor);

            // 実際の代入
            RenderSettings.fogColor = valueColor;
        }
        
    }
}
