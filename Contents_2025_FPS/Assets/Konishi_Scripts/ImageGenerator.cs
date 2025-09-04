using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGenerator : MonoBehaviour
{
    public int imageMax;
    public Canvas canvas;
    public GameObject[] Image;
    ColorManager colorManager;

    void Start()
    {
        colorManager = GetComponent<ColorManager>();
    }

    void Update()
    {

    }
}
