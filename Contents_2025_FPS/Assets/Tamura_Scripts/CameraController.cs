using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //  ------------------------------------------------------変数------------------------------------------------
    [SerializeField] Transform playerBody; // プレイヤー(親オブジェクト)
    [SerializeField] float sensitivity = 2f; // 感度
    float xRotation = 0f; // 上下回転の蓄積
    

    // --------------------------------------------------------関数-----------------------------------------
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを画面中央に固定
        Cursor.visible = false; // マウスカーソルを非表示
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
        xRotation = Mathf.Clamp(xRotation, -60.0f, 60.0f); // 視点の上限下限を設定

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // 回転を適用

        //　プレイヤー本体で左右の回転処理を行う
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
