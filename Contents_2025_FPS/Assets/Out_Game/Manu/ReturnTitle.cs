using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTitle : MonoBehaviour
{
    [SerializeField] private GameObject ReturnTitleButton; // 出したいUIボタン
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab; // 切り替えキー

    void Update()
    {
        // 指定キーを押したらボタンの表示・非表示を切り替える
        if (Input.GetKeyDown(toggleKey))
        {
            ReturnTitleButton.SetActive(!ReturnTitleButton.activeSelf);
        }
    }
}
