using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakarabakoUiScript : MonoBehaviour
{
    [SerializeField] private Transform target; // UIが追従する対象
    [SerializeField] private Vector3 offset = new Vector3(0, 0.2f, 0); // 頭上オフセット
    private void Update()
    {
        if (target != null)
        {
            // 追従
            transform.position = target.position + offset;

            // 常にカメラの方を向く
            transform.forward = Camera.main.transform.forward;
        }
    }
}
