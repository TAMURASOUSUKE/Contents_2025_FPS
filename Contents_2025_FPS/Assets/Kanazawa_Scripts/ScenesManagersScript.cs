using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagersScripts : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    public enum Scene
    { 
        TITLE,
        GAME,
        MENU,
        GAMEOVER,
        CLEAR
    }

    [SerializeField]
    public Scene currentScene = Scene.TITLE;

    [SerializeField] PlayerController playerController; //プレイヤーの現在の数値の取得に使用
    int currentHP;                      //現在の体力の保存で使用

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // フレームレートを固定
        if(GameManager.isFirstPlay == false)
        {
            TItleSceneTransition();
        }
        else
        {
            GameSceneTransition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = playerController.GetHp();   //現在の体力の取得
        if(currentHP <= 0)                      //体力が０以下の時ゲームオーバーシーンに移行
        {
            GameOverSceneTransition();
        }
    }

    public void TItleSceneTransition() //タイトルシーンに遷移
    {
        currentScene = Scene.TITLE;
        uiManager.TitleUiActive();
        CursorMode();
    }
    public void GameSceneTransition() //ゲームシーンに遷移
    {
        currentScene = Scene.GAME;
        uiManager.GameUiActive();
        FpsMode();
    }
    public void MenuSceneTransition() //メニューシーンに遷移
    {
        currentScene = Scene.MENU;
    }
    public void GameOverSceneTransition() //ゲームオーバーシーンに遷移
    {
        currentScene = Scene.GAMEOVER;
        uiManager.GameOverActive();
        CursorMode();
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
 
    private void FpsMode()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを画面中央に固定
        Cursor.visible = false; // マウスカーソルを非表示
    }

    private void CursorMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
