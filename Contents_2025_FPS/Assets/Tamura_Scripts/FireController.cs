using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;
    [SerializeField] Sprite[] sprites = new Sprite[15]; // アニメーションする枚数分用意
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int drawFps;
    int index = 0; // インデックス操作用
    bool isInCamera = false; // カメラ内に入っているか
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (isInCamera)
        {
            FireAnim();
        }
    }

    private void LateUpdate()
    {
        if (isInCamera)
        {
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
