using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    const float INTERACT_DISTANCE = 1.5f;

    [SerializeField]
    GameObject gaugeObj;
    [SerializeField]
    Image gauge;

    Image[] gaugeImages;
    TextMeshProUGUI gaugeText;
    private void Start()
    {
        gaugeImages = gaugeObj.GetComponentsInChildren<Image>();
        gaugeText = gaugeObj.GetComponentInChildren<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        InvisibleGauge();

        //カメラの中心にレイを飛ばす
        Ray mainCamMidRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(mainCamMidRay.origin, mainCamMidRay.direction, Color.black);
        if (Physics.Raycast(mainCamMidRay, out RaycastHit hitInfo))
        {
            //あったオブジェクトがインタラクト可能オブジェクトタグを持つなら
            if (hitInfo.collider.CompareTag("InteractObject"))
            {
                if ((hitInfo.point - transform.position).magnitude <= INTERACT_DISTANCE)
                {
                    IInteractObject interactObject = hitInfo.collider.gameObject.GetComponentInParent<IInteractObject>();
                    Debug.Log(hitInfo.collider.name + interactObject.GetCanInteract());
                    if(interactObject.GetCanInteract() == true)
                    {
                        VisibleGauge();
                        if (Input.GetKey(KeyCode.E))
                        {
                            gauge.fillAmount += 0.01f;
                            if (gauge.fillAmount >= 1)
                            {
                                interactObject.OnTriggerInteract();
                                gauge.fillAmount = 0;
                            }
                        }
                        else if (gauge.fillAmount > 0)
                        {
                            gauge.fillAmount -= 0.01f;
                        }
                    }
                }
                else if (gauge.fillAmount > 0)
                {
                    gauge.fillAmount = 0;
                }
            }
        }
    }

    private void InvisibleGauge()
    {
        foreach(Image image in gaugeImages)
        {
            image.enabled = false;
        }
        gaugeText.enabled = false;
    }

    private void VisibleGauge()
    {
        foreach (Image image in gaugeImages)
        {
            image.enabled = true;
        }
        gaugeText.enabled = true;
    }
}
