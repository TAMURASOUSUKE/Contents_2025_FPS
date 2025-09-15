using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackImageController : MonoBehaviour
{
    [SerializeField] Texture[] backImages = new Texture[15]; // 動かすもやの枚数
    [SerializeField] int fps = 2;
    MaterialPropertyBlock mpb; // レンダラーのプロパティだけを上書きする
    MeshRenderer meshRenderer;
    int index = 0;

    void Awake()
    {
        // Renderer を確保
        if (!meshRenderer && !TryGetComponent(out meshRenderer))
        {
            Debug.LogError("Renderer が見つかりません。Inspector で割り当てるか同一GameObjectに付けてください。", this);
        }
        // MPB を最速で生成（Start 依存にしない）
        mpb = new MaterialPropertyBlock();
    }


    void Start()
    {
        mpb =  new MaterialPropertyBlock();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Time.frameCount % fps == 0)
        {
            index = (index + 1) % backImages.Length; // 配列の長さだけ回す
            meshRenderer.GetPropertyBlock(mpb); // 現在のプロパティ設定をmbpにコピーする
            mpb.SetTexture("_BaseMap", backImages[index]); // テクスチャに変更を加える
            meshRenderer.SetPropertyBlock(mpb);  // 変更を適用する
        }
    }
}
