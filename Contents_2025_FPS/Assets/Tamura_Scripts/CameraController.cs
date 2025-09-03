using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //  ------------------------------------------------------変数------------------------------------------------
    [SerializeField] Transform playerBody; // プレイヤー(親オブジェクト)
    [SerializeField] Transform cameraPivot; // カメラの基点
    [SerializeField] float topCameraLimit = 0.0f; // カメラの上側向きの制限
    [SerializeField] float bottomCameraLimit = 0.0f; //　カメラの下側の向きの制限
    [SerializeField] float sensitivity = 2f; // 感度
    PlayerController playerController;
    float xRotation = 0f; // 上下回転の蓄積
    float timer = 0f; // ダメージ演出の時間


    // --------------------------------------------------------関数-----------------------------------------
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを画面中央に固定
        Cursor.visible = false; // マウスカーソルを非表示
        playerController = playerBody.GetComponent<PlayerController>(); // プレイヤーのスクリプトを取得
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity; // x軸方向に感度をかける
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity; // y軸方向に感度をかける

        //　上下の回転処理
        xRotation -= mouseY;
        // 視点の上限下限を設定
        xRotation = Mathf.Clamp(xRotation, topCameraLimit, bottomCameraLimit); // X軸回転は上に行くほど負の値を出す

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // 回転を適用

        //　プレイヤー本体で左右の回転処理を行う
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

