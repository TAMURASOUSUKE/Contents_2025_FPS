using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    [SerializeField] ColorManager Color;
    Image image;
    float duration;         //フィルターのクールタイム
    float speed;
    float maxRotate = -90f;
    float currentRotate;
    bool removeFilter;
    // Start is called before the first frame update
    void Start()
    {
        duration = Color.filterCoolTime;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.rectTransform.localEulerAngles = new Vector3(0, 0, currentRotate);
        removeFilter = Color.removeFilter;
        if (removeFilter)
        {
            speed = Mathf.Abs(maxRotate) / duration;   // ← 必ず正の値にする
            currentRotate = Mathf.MoveTowards(currentRotate, maxRotate, speed * Time.deltaTime);
        }
        else
        {
            currentRotate = 0;
        }
    }
}
