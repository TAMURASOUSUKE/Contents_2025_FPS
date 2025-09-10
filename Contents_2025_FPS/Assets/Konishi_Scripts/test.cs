using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        float speed = 10f;
        image.rectTransform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
