using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    const float COLLIDER_OFFSET = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            //カメラの中心にレイを飛ばす
            Ray mainCamMidRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(mainCamMidRay.origin, mainCamMidRay.direction, Color.black);
            if (Physics.Raycast(mainCamMidRay, out RaycastHit hitInfo))
            {
                //あったオブジェクトが宝箱タグを持つなら
                if (hitInfo.collider.CompareTag("Takarabako"))
                {
                    float maxLength = hitInfo.collider.gameObject.GetComponentInParent<SphereCollider>().radius;
                    if ((hitInfo.collider.transform.position - transform.position).magnitude <= maxLength + COLLIDER_OFFSET)
                    {
                        hitInfo.collider.gameObject.GetComponentInParent<TakarabakoScript>().UpGuage();
                    }
                }
            }
        }
    }
}
