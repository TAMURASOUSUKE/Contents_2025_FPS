using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireController : MonoBehaviour
{

    [SerializeField] Sprite[] sprites = new Sprite[15]; // アニメーションする枚数分用意
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int drawFps = 2;
    [SerializeField] float near = 4f;
    [SerializeField] float far = 8f;
    Camera cam; // メインカメラ
    GameObject player; // プレイヤー
    Color renderColor;
    int index = 0; // インデックス操作用
    bool isInCamera = false; // カメラ内に入っているか
    float renderDistance = 10.0f; // 描画し始める距離
    void Start()
    {
        cam = Camera.main; // メインカメラを探す
        player = GameObject.FindWithTag("Player"); // タグでプレイヤーを探す
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (isInCamera)
        {
            RenderFire();
            FireAnim();
        }
       
    }

    private void LateUpdate()
    {
        if (isInCamera)
        {
            RenderFire();
            LookAnim();
        }
       
    }


    void FireAnim()
    {
        // 2フレームごとに描画
        if (Time.frameCount % drawFps == 0)
        {
            index = (index + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[index];
        }
    }


    void LookAnim()
    {
        // 炎からプレイヤーの方向を取得
        Vector3 dir = player.transform.position - gameObject.transform.position;
        dir.y = 0; //Y軸以外の回転は無視する

        if (dir.sqrMagnitude > 0.0001) // 0割り防止
        {
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

    }

    void  RenderFire()
    {
        Vector3 targetPos = player.transform.position; // プレイヤーの位置
        Vector3 firePos = gameObject.transform.position; // 炎の位置

        float d2 = (targetPos - firePos).sqrMagnitude; // 距離を2乗した値を出す
        float a = 1f - Mathf.InverseLerp(near * near, far * far, d2); // 1 -...で値を反転させる

        var c = spriteRenderer.color;
        c.a = a;
        spriteRenderer.color = c;
    }

    // 写っていない時はフラグを立てない
    private void OnBecameInvisible()
    {
        isInCamera = false;
    }

    // 写っているときはフラグを立てる
    private void OnBecameVisible()
    {
        isInCamera = true;
    }

}
