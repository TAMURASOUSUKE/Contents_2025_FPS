using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//  プレイヤーの移動を担当する(FPSなのでカメラにつける)
public class PlayerController : MonoBehaviour
{
    // ------------------------------------------変数---------------------------------
    [SerializeField] ScenesManagersScripts scenesManagers;
    [SerializeField] Transform cameraTransform; // しゃがむ際のカメラ移動に使う
    [SerializeField] GameObject camera;
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
    [SerializeField] float damageSpeedTimer = 3f; // 足が遅くなる秒数
    List<GameObject> keys = new List<GameObject>(); // 鍵用
    CapsuleCollider capsuleCollider; // しゃがみに使う
    Rigidbody rb; // 移動に使う
    Vector3 moveDir = Vector3.zero; // 移動方向
    Vector3 moveValue = Vector3.zero; // 移動する量
    float currentSpeed = 0.0f; // 現在のスピードを取得
    float baseMoveSpeed = 0;
    float baseCrouchSpeed = 0;
    float baseDashSpeed = 0;
    float climbingY = 0.0f; // 梯子を上るとき用の変数
    float lastLadderCancelTime = int.MinValue; // キャンセルした時間を記録する変数
    float slowFactor = 1f; // 1通常　0.5半分 0完全停止
    float slowTimer = 0f; // 減速の残り時間
    bool isCrouch = false; // しゃがみ用のフラグ
    bool isDash = false; // ダッシュ用のフラグ
    bool isCliming = false; // 梯子を上るとき用のフラグ
    bool isTake = false; // スピード補正をかけたかどうか
    bool isGround = false; // 地面に接触しているかどうか
    // ------------------------------------------関数---------------------------------------

    

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyを取得
        capsuleCollider = rb.GetComponent<CapsuleCollider>(); // カプセルコライダーを取得

        // もともとの速度を保存しておく
        baseMoveSpeed = moveSpeed;
        baseCrouchSpeed = crouchMoveSpeed;
        baseDashSpeed = dashMoveSpeed;
    }


    // 入力処理はUpdate
    void Update()
    {
        if(scenesManagers.currentScene == ScenesManagersScripts.Scene.GAME)
        {
            InputMove();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
            LimitHp();
        ChangeSpeed();
        RecoveryHp();
    }

    //物理挙動はFixedUpdateで分ける
    void FixedUpdate()
    {
        if(scenesManagers.currentScene == ScenesManagersScripts.Scene.GAME)
        {
            CalculateMove();
        }
    }

    // Hpを取得する用
    public int GetHp()
    {
        return hp;
    }

    // Hp変更用(中身を変える)
    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
    public void SetSpeed(float adj)
    {
        // それぞれのスピードを調整
        slowFactor = Mathf.Clamp01(adj);
        slowTimer = damageSpeedTimer;
    }

    void LimitHp()
    {
        if (hp <= 0)
        {
            camera.AddComponent<Rigidbody>();
             
            if (camera.TryGetComponent<Rigidbody>(out var component))
            {
                component = camera.GetComponent<Rigidbody>();
                component.AddForce(Vector3.forward * 0.1f, ForceMode.Force);
            }
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
                currentSpeed = baseCrouchSpeed * slowFactor;
                capsuleCollider.height = crouchHeight;
                capsuleCollider.center = crouchCenter;
            }
            else if (isDash)
            {
                currentSpeed = baseDashSpeed * slowFactor;
                capsuleCollider.height = standHeight;
                capsuleCollider.center = standCenter;
            }
            else
            {
                currentSpeed = baseMoveSpeed * slowFactor;
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

                // 入力がないときは速度を0にする
                if (dirX == 0 && dirZ == 0 && isGround)
                {
                    rb.velocity = Vector3.zero;
                }
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


    public void DestroyRigidbody()
    {
        if (camera.TryGetComponent<Rigidbody>(out var rb))
        {
            Destroy(rb);
            
        }
        camera.transform.Rotate(Vector3.zero);
    }

    // Hpの自動回復
    void RecoveryHp()
    {
        if (Time.frameCount % 180 == 0)
        {
            hp++;
            if (hp >= 300)
            {
                hp = 300;
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

    // ダメージを受けたときに
    void ChangeSpeed()
    {
        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                slowFactor = 1f; // 解除
                slowTimer = 0f;
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

    //　頭上にものがあれば立てない
    bool CanStandUp()
    {
        float headClearance = 0.1f; // 少し余裕を持たせる
        Vector3 rayOrigin = transform.position + capsuleCollider.center; // キャラ中心
        float rayLength = (standHeight - crouchHeight) + headClearance; // 必要な高さ
        int exclude = LayerMask.GetMask("Camera");


        // デバッグ用に Ray を可視化（赤:障害物あり, 緑:クリア）
        bool hit = Physics.Raycast(rayOrigin, Vector3.up, rayLength, ~exclude, QueryTriggerInteraction.Ignore);
        Color rayColor = hit ? Color.red : Color.green;
        Debug.DrawRay(rayOrigin, Vector3.up * rayLength, rayColor, 0.1f); // 0.1秒表示

        return !hit; // ヒットしてなければ立てる
    }

    // 重力操作(坂を登るとき滑らないようにするため)
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rb.useGravity = false;
            isGround = true;
        }

        // テスト用
        if (other.gameObject.CompareTag("Key"))
        {
            keys.Add(other.gameObject);
            other.gameObject.SetActive(false);
            Debug.Log("取得した");
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rb.useGravity = true;
            isGround = false;
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
