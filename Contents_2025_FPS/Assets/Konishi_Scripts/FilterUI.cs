using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterUI : MonoBehaviour
{
    [SerializeField] ColorManager Color;
    Image image;
    public float redRatation = 15f;
    public float greenRatation = 260f;
    public float blueRatation = 135f;
    bool isRedColor = false;
    bool isGreenColor = false;
    bool isBlueColor = false;
    bool isWhiteColor = false;



    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        isRedColor = true;
        isGreenColor = true;
        isBlueColor = true;
        isWhiteColor = true;

        float speed = 0f;
        image.rectTransform.Rotate(0, 0, speed * Time.deltaTime);

    }
}

