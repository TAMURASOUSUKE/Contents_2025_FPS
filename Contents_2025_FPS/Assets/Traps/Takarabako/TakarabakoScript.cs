using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class TakarabakoScript : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    Image image;
    [SerializeField]
    GameObject gauge;
    [SerializeField]
    GameObject key;

    bool isOpen = false; //宝箱が開かれたかどうか
    bool isInteract = false;
    
    [SerializeField]
    GameObject gameManager;

    [SerializeField]
    GameManager.KeyType type;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {  
        if(isOpen == false)
        {
            if (isInteract == false && image.fillAmount > 0)
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

        isInteract = false;
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
    //アニメーションの進み具合で鍵の動きを開始させる
    private void OnAnimationEnd()
    {
        key.GetComponent<KeyOpenMove>().SetIsOpen();
    }
    public void UpGuage()
    {
        isInteract = true;
        image.fillAmount += 0.01f;
    }
}
