using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class TakarabakoScript : MonoBehaviour,IInteractObject
{
    Animator animator;
    [SerializeField]
    GameObject key;

    bool canIntract = true; //宝箱が開かれたかどうか
    
    [SerializeField]
    GameObject gameManager;

    [SerializeField]
    GameManager.KeyType type;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Open()
    {
        //宝箱を開ける
        canIntract = false;
        animator.SetTrigger("PlayerInteract");
        gameManager.GetComponent<GameManager>().GetKey(type);
    }
    //アニメーションの進み具合で鍵の動きを開始させる
    private void OnAnimationEnd()
    {
        key.GetComponent<KeyOpenMove>().SetIsOpen();
    }
    public void OnTriggerInteract()
    {
        Open();
    }
    public bool GetCanInteract()
    {
        return canIntract;
    }
}
