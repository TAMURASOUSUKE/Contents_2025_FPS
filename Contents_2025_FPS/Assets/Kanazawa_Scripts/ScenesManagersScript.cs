using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagersScripts : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    public enum Scene
    { 
        TITLE,
        GAME,
        MENU,
        GAMEOVER,
        CLEAR
    }

    public Scene currentScene = Scene.TITLE;

    [SerializeField] PlayerController playerController; //プレイヤーの現在の数値の取得に使用
    int currentHP;                      //現在の体力の保存で使用

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // フレームレートを固定
        TItleSceneTransition();
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

    public void TItleSceneTransition() //タイトルシーンに遷移
    {
        currentScene = Scene.TITLE;
        uIManager.TitleUiSetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void GameSceneTransition() //ゲームシーンに遷移
    {
        currentScene = Scene.GAME;
        uIManager.GameUiSetActive(true);
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを画面中央に固定
        Cursor.visible = false; // マウスカーソルを非表示
    }
    public void MenuSceneTransition() //メニューシーンに遷移
    {
        currentScene = Scene.MENU;
    }
    public void GameOverSceneTransition() //ゲームオーバーシーンに遷移
    {
        currentScene = Scene.GAMEOVER;
    }
    public void ClearSceneTransition() //クリアーシーンに遷移
    {
        currentScene = Scene.CLEAR;
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
