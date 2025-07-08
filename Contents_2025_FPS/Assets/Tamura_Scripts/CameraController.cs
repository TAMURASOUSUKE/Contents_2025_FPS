using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //  ------------------------------------------------------変数------------------------------------------------
    [SerializeField] Transform playerBody; // プレイヤー(親オブジェクト)
    [SerializeField] Transform cameraPivot; // カメラの基点
    [SerializeField] float sensitivity = 2f; // 感度
    [SerializeField] float cameraCheckDistance = 0.2f; // カメラが壁にめり込むのを防ぐ距離
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

    void LateUpdate()
    {
        PreventCameraClipping();
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

    // カメラが壁を突き抜けないように制御
    void PreventCameraClipping()
    {
        Vector3 origin = cameraPivot.position;
        Vector3 direction = transform.position - origin;
        float distance = direction.magnitude;

        Ray ray = new Ray(origin, direction.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, distance, ~0, QueryTriggerInteraction.Ignore))
        {
            // 壁があったら、カメラを壁の手前に置く
            transform.position = hit.point - direction.normalized * cameraCheckDistance;
        }
        else
        {
            // 壁がなければ、元の位置に戻す
            transform.position = origin + direction.normalized * distance;
        }
    }

    // デバッグ用：Rayの可視化
    void OnDrawGizmos()
    {
        if (cameraPivot != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(cameraPivot.position, transform.position);
        }
    }
}

