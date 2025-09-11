using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterUI : MonoBehaviour
{
    [SerializeField] ColorManager Color;
    Image image;
    Animator animator;
    float speed;
    float targetRotate;
    float currentRotate;
    float duration = 0.2f;
    public float spinSpeed = 1500f;
    public float redRatation = 0f;
    public float greenRatation = 240f;
    public float blueRatation = 118f;
    bool isRedColor = false;
    bool isGreenColor = false;
    bool isBlueColor = false;
    bool removeFilter = false;
    bool canFilterChange;


    void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Initialize();
        Target();
        UIChange(); 
        image.rectTransform.localEulerAngles = new Vector3(0, 0, currentRotate);

    }
    void UIChange()
    {
        speed = spinSpeed / duration;
        currentRotate = Mathf.MoveTowards(currentRotate, targetRotate, speed * Time.deltaTime);
        if (removeFilter)
        {
            animator.SetBool("isCoolTime", true);
        }
        else
        {
            animator.SetBool("isCoolTime", false);
        }
    }
    void Target()
    {
        if (isRedColor)
        {
            targetRotate = redRatation;
        }
        else if (isGreenColor)
        {
            targetRotate = greenRatation;
        }
        else if (isBlueColor)
        {
            targetRotate = blueRatation;
        }
    }
    void Initialize()
    {
        isRedColor = Color.isRed;
        isGreenColor = Color.isGreen;
        isBlueColor = Color.isBlue;
        removeFilter = Color.removeFilter;
        canFilterChange = Color.canFilterChange;
    }
}

