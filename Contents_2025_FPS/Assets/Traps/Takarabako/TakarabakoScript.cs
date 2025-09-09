using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TakarabakoScript : MonoBehaviour
{
    SphereCollider InteractArea;
    Animator animator;
    [SerializeField]
    Image image;
    [SerializeField]
    GameObject gauge;
    [SerializeField]
    GameObject key;

    bool isOpen = false; //宝箱が開かれたかどうか

    [SerializeField]
    GameObject gameManager;

    [SerializeField]
    GameManager.KeyType type;

    private void Start()
    {
        InteractArea = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {  
        if(isOpen == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                //カメラの中心にレイを飛ばす
                Ray mainCamMidRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                if (Physics.Raycast(mainCamMidRay, out RaycastHit hitInfo, InteractArea.radius))
                {
                    //あったオブジェクトが宝箱タグを持つなら
                    if (hitInfo.collider.CompareTag("Takarabako"))
                    {
                        //ゲージを上昇
                        hitInfo.collider.gameObject.GetComponentInParent<TakarabakoScript>().image.fillAmount += 0.01f;
                    }
                }
            }
            else if (image.fillAmount > 0)
            {
                //ゲージの上昇中以外はゲージが減少する
                image.fillAmount -= 0.01f;
            }

        }

        //ゲージがマックスなら宝箱を開ける
        if (image.fillAmount >= 1)
        {           
            Open();
        }
    }

    private void Open()
    {
        //宝箱を開ける
        isOpen = true;
        animator.SetTrigger("PlayerInteract");
        gameManager.GetComponent<GameManager>().GetKey(type);
    }

    private void OnTriggerEnter(Collider other)
    {
        //ゲージのUIが一定の距離内の時だけ描画させる
        gauge.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        //離れると描画しないようにする
        gauge.SetActive(false);
    }
    private void OnAnimationEnd()
    {
        key.GetComponent<KeyOpenMove>().SetIsOpen();
    }
}
