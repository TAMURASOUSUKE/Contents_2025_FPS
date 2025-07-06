using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  プレイヤーの移動を担当する(FPSなのでカメラにつける)
public class PlayerController : MonoBehaviour
{
    // ------------------------------------------変数---------------------------------

    [SerializeField] Transform cameraTransform; // しゃがむ際のカメラ移動に使う
    [SerializeField] Vector3 standCenter = new Vector3(0, 1.0f, 0); // 立っている時の判定の中心
    [SerializeField] Vector3 crouchCenter = new Vector3(0, 0.5f, 0); // しゃがんでいるときの判定の中心
    [SerializeField] float moveSpeed = 1.0f; // 通常の移動速度
    [SerializeField] float standHeight = 1.7f; // 立っている時の高さ
    [SerializeField] float crouchMoveSpeed = 1.0f; //　しゃがんだ時の移動速度
    [SerializeField] float crouchHeight = 1.0f; // しゃがんだ時の高さ
    [SerializeField] float crouchSpeed = 1.0f;　//　しゃがむときのスピード
    [SerializeField] float dashMoveSpeed = 1.0f; // ダッシュ時の移動速度
    [SerializeField] float jumpHeight = 1.0f; // ジャンプの高さ
    [SerializeField] float standCameraY = 1.7f; // 通常のカメラの高さ
    [SerializeField] float crouchCameraY = 0.8f; // しゃがんだ時のカメラの高さ
    CapsuleCollider capsuleCollider; // しゃがみに使う
    Rigidbody rb; // 移動に使う
    Vector3 moveDir = Vector3.zero; // 移動方向
    Vector3 moveValue = Vector3.zero; // 移動する量
    Vector3 defaultScale = Vector3.one; // 通常状態の大きさ(しゃがみ時に参照)
    float currentSpeed = 0.0f; // 現在のスピードを取得
    bool isJump = false; // ジャンプ用のフラグ
    bool isCrouch = false; // しゃがみ用のフラグ

    // ------------------------------------------関数---------------------------------------


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyを取得
        capsuleCollider = rb.GetComponent<CapsuleCollider>(); // カプセルコライダーを取得　　
    }


    // 入力処理はUpdate
    private void Update()
    {
        InputMove();
        InputJump();
    }

    //物理挙動はFixedUpdateで分ける
    void FixedUpdate()
    {
        CalculateMove();
    }



    //  移動に関する入力を受け付ける
    void InputMove()
    {
        moveDir = Vector3.zero; // 移動方向
        moveValue = Vector3.zero; // 移動する量

        if (this.gameObject != null)
        {
            // 各状態を設定 (通常移動, ダッシュ, しゃがみ) 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = dashMoveSpeed; // ダッシュ用のスピード
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouch = true;
                currentSpeed = crouchMoveSpeed; // しゃがみ用のスピード
                // 各変数をカプセルコライダーに適用
                capsuleCollider.height = crouchHeight;
                capsuleCollider.center = crouchCenter;
            }
            else
            {
                isCrouch = false; // しゃがんでいない時はfalse
                currentSpeed = moveSpeed; // 通常のスピード
                // 各変数をカプセルコライダーに適用
                capsuleCollider.height = standHeight;
                capsuleCollider.center = standCenter;
            }

            CrouchCamera();

            // 移動方向を設定
            float dirX = Input.GetAxisRaw("Horizontal");
            float dirZ = Input.GetAxisRaw("Vertical");
            moveDir = new Vector3(dirX, 0, dirZ); // 入力された値を方向として設定
            moveDir.Normalize(); // 正規化
        }
    }

    // 実際の移動を計算
    void CalculateMove()
    {
        if (gameObject != null)
        {
            moveValue = moveDir * currentSpeed;
            rb.velocity = new Vector3(moveValue.x, rb.velocity.y, moveValue.z); // 移動量を代入
        }
    }

    //　カメラのしゃがみ挙動を制御する
    void CrouchCamera()
    {
        float tagetY = isCrouch ? crouchCameraY : standCameraY; // しゃがみフラグがtrueかfalseか判断
        Vector3 currentPos = cameraTransform.localPosition; // 現在のカメラの位置を取得
        float newY = Mathf.Lerp(currentPos.y, tagetY, Time.deltaTime * crouchSpeed); // targerYまでの値を補完する(crouchSpeedの値で動かす)
        cameraTransform.localPosition = new Vector3 (currentPos.x, newY, currentPos.z); // カメラの位置を設定
    }


    //　ジャンプ用の入力
    void InputJump()
    {
        if (isJump) return; // すでにジャンプ中なら終了する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isJump = true;
        }
    }

    //　着地用
    void OnCollisionEnter(Collision other)
    {
        if(!isJump) return; // ジャンプ中でないなら終了
        if (other.gameObject.CompareTag("Ground")) // フィールドのタグをGroundにしているがフィールド班の要望によって変更可
        {
            isJump = false;
        }
    }
}
