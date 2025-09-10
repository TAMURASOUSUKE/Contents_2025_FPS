using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterUI : MonoBehaviour
{
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        float speed = 0f;
        image.rectTransform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
