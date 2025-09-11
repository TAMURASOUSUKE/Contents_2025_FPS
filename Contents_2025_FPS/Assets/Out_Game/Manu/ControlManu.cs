using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManu : MonoBehaviour
{
    [SerializeField] private GameObject ControlManuButton; // 出したいUIボタン
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab; // 切り替えキー

    void Update()
    {
        // 指定キーを押したらボタンの表示・非表示を切り替える
        if (Input.GetKeyDown(toggleKey))
        {
            ControlManuButton.SetActive(!ControlManuButton.activeSelf);
        }
    }
}
