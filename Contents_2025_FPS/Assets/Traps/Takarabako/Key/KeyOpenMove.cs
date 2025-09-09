using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpenMove : MonoBehaviour
{
    public bool isOpen = false;
    public float targetHeight;  // 高さ
    public float speed;        // 速度
    public float stopTime;        // 移動終了時間
    public float endTime;        // 削除時間
    private Vector3 startPos;       //初期位置
    private Vector3 targetPos;      //目標座標
    private float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * targetHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen == true)
        {
            t += Time.deltaTime * speed;
            if (t < stopTime)
            {
                t += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, targetPos, t);
            }
            if(t > endTime)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetIsOpen()
    {
        isOpen = true;
    }
}
