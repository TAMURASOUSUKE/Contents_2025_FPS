using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//  プレイヤーの移動を担当する(FPSなのでカメラにつける)
public class PlayerController : MonoBehaviour
{
    // ------------------------------------------変数---------------------------------

    [SerializeField] Transform cameraTransform; // しゃがむ際のカメラ移動に使う
    [SerializeField] Vector3 standCenter = new Vector3(0, 1.0f, 0); // 立っている時の判定の中心
    [SerializeField] Vector3 crouchCenter = new Vector3(0, 0.5f, 0); // しゃがんでいるときの判定の中心
    [SerializeField] int hp = 100;
    [SerializeField] float moveSpeed = 1.0f; // 通常の移動速度
    [SerializeField] float standHeight = 1.5f; // 立っている時の高さ
    [SerializeField] float crouchMoveSpeed = 1.0f; //　しゃがんだ時の移動速度
    [SerializeField] float crouchHeight = 1.0f; // しゃがんだ時の高さ
    [SerializeField] float crouchSpeed = 1.0f;　//　しゃがむときのスピード
    [SerializeField] float dashMoveSpeed = 1.0f; // ダッシュ時の移動速度
    [SerializeField] float jumpHeight = 1.0f; // ジャンプの高さ
    [SerializeField] float standCameraY = 1.5f; // 通常のカメラの高さ
    [SerializeField] float crouchCameraY = 0.8f; // しゃがんだ時のカメラの高さ
    [SerializeField] float ladderReenterDelay = 0.5f; // 梯子に再度上れるようになる時間
    CapsuleCollider capsuleCollider; // しゃがみに使う
    Rigidbody rb; // 移動に使う
    Vector3 moveDir = Vector3.zero; // 移動方向
    Vector3 moveValue = Vector3.zero; // 移動する量
    float currentSpeed = 0.0f; // 現在のスピードを取得
    float climbingY = 0.0f; // 梯子を上るとき用の変数
    float lastLadderCancelTime = int.MinValue; // キャンセルした時間を記録する変数
    bool isJump = false; // ジャンプ用のフラグ
    bool isCrouch = false; // しゃがみ用のフラグ
    bool isDash = false; // ダッシュ用のフラグ
    bool isCliming = false; // 梯子を上るとき用のフラグ
    // ------------------------------------------関数---------------------------------------


    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyを取得
        capsuleCollider = rb.GetComponent<CapsuleCollider>(); // カプセルコライダーを取得
    }


    // 入力処理はUpdate
    void Update()
    {
        
        InputMove();
        InputJump();
        LimitHp();
    }

    //物理挙動はFixedUpdateで分ける
    void FixedUpdate()
    {
        CalculateMove();
    }

    // Hpを取得する用
    public int GetHp()
    {
        return hp;
    }

    // Hp変更用(中身を変える)
    public void SetHp(int hp)
    {
        this.hp = this.hp - hp;
    }

    void LimitHp()
    {
        if (hp <= 0)
        {
            hp = 0;
        }
    }
    //  移動に関する入力を受け付ける
    void InputMove()
    {
        moveDir = Vector3.zero; // 移動方向
        moveValue = Vector3.zero; // 移動する量

        if (this.gameObject != null)
        {


            // 状態切り替え：キーを押した「瞬間」でのみ判定
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isCliming)
            {
                isCrouch = true;
                isDash = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (CanStandUp()) // 頭上に障害がないときのみダッシュに移行
                {
                    isDash = true;
                    isCrouch = false;
                }
            }

            // 両方のキーが離されたら、通常状態へ
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                if (CanStandUp())
                {
                    isCrouch = false;
                    isDash = false;
                }
            }

            // スピードとコライダーの設定
            if (isCrouch) // (梯子を上るときはしゃがめないようにする)
            {
                currentSpeed = crouchMoveSpeed;
                capsuleCollider.height = crouchHeight;
                capsuleCollider.center = crouchCenter;
            }
            else if (isDash)
            {
                currentSpeed = dashMoveSpeed;
                capsuleCollider.height = standHeight;
                capsuleCollider.center = standCenter;
            }
            else
            {
                currentSpeed = moveSpeed;
                capsuleCollider.height = standHeight;
                capsuleCollider.center = standCenter;
            }


            CrouchCamera();

            // 梯子に接触していないなら
            if (!isCliming)
            {
                // 移動方向を設定
                float dirX = Input.GetAxisRaw("Horizontal");
                float dirZ = Input.GetAxisRaw("Vertical");
                //　カメラの向いている方向に進行方向を合わせる(後ろ、左はマイナスの値として評価する)
                Vector3 forward = cameraTransform.forward; // カメラの前方向の値を取得
                Vector3 right = cameraTransform.right; // カメラの右方向の値を取得
                                                       // y軸方向には関与しない(水平)
                forward.y = 0;
                right.y = 0;

                // 個別の方向を正規化
                forward.Normalize();
                right.Normalize();

                // 移動ベクトルの計算
                moveDir = (dirZ * forward + dirX * right).normalized;
            }
            // 梯子を上るとき用
            else
            {
                climbingY = Input.GetAxis("Vertical"); // 上下方向
                // 登っているときに左Controlで梯子解除
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    CancelClimbing();
                }
            }


        }
    }

   

    // 実際の移動を計算
    void CalculateMove()
    {
        if (gameObject != null)
        {
            if (!isCliming)
            {
                moveValue = moveDir * currentSpeed;
                rb.velocity = new Vector3(moveValue.x, rb.velocity.y, moveValue.z); // 移動量を代入
            }
            else
            {
                // rb.useGravity = false; // 落下しないようにする
                rb.velocity = new Vector3(0, climbingY * currentSpeed, 0);
            }

        }
    }

    //　カメラのしゃがみ挙動を制御する
    void CrouchCamera()
    {
        float tagetY = isCrouch ? crouchCameraY : standCameraY; // しゃがみフラグがtrueかfalseか判断
        Vector3 currentPos = cameraTransform.localPosition; // 現在のカメラの位置を取得
        float newY = Mathf.Lerp(currentPos.y, tagetY, Time.deltaTime * crouchSpeed); // targerYまでの値を補完する(crouchSpeedの値で動かす)
        cameraTransform.localPosition = new Vector3(currentPos.x, newY, currentPos.z); // カメラの位置を設定
    }


    //　ジャンプ用の入力
    void InputJump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !isJump && !isCliming)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isJump = true;
        }
    }

    //　頭上にものがあれば立てない
    bool CanStandUp()
    {
        float headClearance = 0.1f; // 少し余裕を持たせる
        Vector3 rayOrigin = transform.position + capsuleCollider.center; // キャラ中心
        float rayLength = (standHeight - crouchHeight) + headClearance; // 必要な高さ

        // デバッグ用に Ray を可視化（赤:障害物あり, 緑:クリア）
        bool hit = Physics.Raycast(rayOrigin, Vector3.up, rayLength, ~0, QueryTriggerInteraction.Ignore);
        Color rayColor = hit ? Color.red : Color.green;
        Debug.DrawRay(rayOrigin, Vector3.up * rayLength, rayColor, 0.1f); // 0.1秒表示

        return !hit; // ヒットしてなければ立てる
    }

  

    //　着地用
    void OnCollisionEnter(Collision other)
    {
        if (!isJump) return; // ジャンプ中でないなら終了
        if (other.gameObject.CompareTag("Ground")) // フィールドのタグをGroundにしているがフィールド班の要望によって変更可
        {
            isJump = false;
        }
    }



    // 梯子用

    // 梯子キャンセル用
    void CancelClimbing()
    {
        isCliming = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        lastLadderCancelTime = Time.time; // キャンセルした時間を記録

        // 障害物にめり込まないように少し手前に押し戻す（オプション）
        Vector3 backDirection = -cameraTransform.forward;
        transform.position += backDirection * 0.01f; // 小さく後ろに動かす
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // 再び登れるようになるまで時間制限を設ける
            if (Time.time - lastLadderCancelTime < ladderReenterDelay)
            {
                return;
            }


            float vertical = Input.GetAxis("Vertical");
            if (vertical != 0f && !isCliming)
            {
                isCliming = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                Debug.Log("梯子に入った（登り/降り）");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LadderBottom"))
        {
            // Sキー（下方向）を押していないなら、Climbing中断しない
            float vertical = Input.GetAxis("Vertical");

            // 登ってる状態で、入力が上なら Climbing継続、それ以外なら中断
            if (isCliming && vertical <= 0f)
            {
                isCliming = false;
                rb.useGravity = true;
                Debug.Log("下端で降りたよ");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LadderTop"))
        {
            float vertical = Input.GetAxis("Vertical");
            if (isCliming && vertical > 0f)
            {
                isCliming = false;
                rb.useGravity = true;
                rb.velocity = cameraTransform.forward * moveSpeed;
                Debug.Log("梯子の上に到達（前進）");
            }
        }
    }

}
