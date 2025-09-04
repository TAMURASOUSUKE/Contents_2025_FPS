using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagersScripts : MonoBehaviour
{
    [SerializeField] PlayerController playerController; //プレイヤーの現在の数値の取得に使用
    int currentHP;                      //現在の体力の保存で使用

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // フレームレートを固定
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = playerController.GetHp();   //現在の体力の取得
        if(currentHP <= 0)                      //体力が０以下の時ゲームオーバーシーンに移行
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))         //ゴール判定のコライダーにプレイヤーが接触したときゴールシーンに移行
        {
           Debug.Log("Goal");
           SceneManager.LoadScene("GoalScene");
        }

    }
 
}
