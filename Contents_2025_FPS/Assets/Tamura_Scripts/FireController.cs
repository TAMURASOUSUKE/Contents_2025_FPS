using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;
    [SerializeField] Sprite[] sprites = new Sprite[15]; // アニメーションする枚数分用意
    [SerializeField] SpriteRenderer spriteRenderer;
    int index = 0; // インデックス操作用
    bool isInCamera = false; // カメラ内に入っているか
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        isInCamera = IsVisibleFrom(spriteRenderer, cam);
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
        if (Time.frameCount % 2 == 0)
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

    bool IsVisibleFrom(Renderer rend, Camera cam)
    {
        if(!rend || !cam) return false;
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, rend.bounds);
    }
}
