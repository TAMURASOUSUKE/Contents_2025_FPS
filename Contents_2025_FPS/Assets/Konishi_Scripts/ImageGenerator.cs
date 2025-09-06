using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGenerator : MonoBehaviour
{
    public int imageMax;
    float imageTimer;
    float imageCreateTime = 2f;
    public Canvas canvas;
    public GameObject[] image;
    ColorManager colorManager;

    void Start()
    {
        colorManager = FindObjectOfType<ColorManager>();
    }

    void Update()
    {
        ImageCreate();
    }

    void ImageCreate()
    {
        if (colorManager.isColorChange)
        {
            imageTimer += Time.deltaTime;
            if (imageTimer > imageCreateTime)
            {
                int index = Random.Range(0, image.Length);
                GameObject prefab = image[index];
                GameObject newImage = Instantiate(prefab, canvas.transform);
                RectTransform rt = newImage.GetComponent<RectTransform>();
                rt.anchoredPosition = Vector2.zero;
                imageTimer = 0;
            }
        }
    }

}
