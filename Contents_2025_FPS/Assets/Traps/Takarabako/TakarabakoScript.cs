using System.Drawing;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class TakarabakoScript : MonoBehaviour,IInteractObject
{
    Animator animator;
    [SerializeField]
    GameObject key;

    GameObject instanceKey;

    Vector3 offset = new Vector3(0, 0.5f, 0);

    bool canIntract = true; //宝箱が開かれたかどうか
    
    [SerializeField]
    GameObject gameManager;

    [SerializeField]
    GameManager.KeyType type;
    private void Start()
    {
        animator = GetComponent<Animator>();
        instanceKey = Instantiate(key, transform.position + offset, transform.rotation);
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
        instanceKey.GetComponent<KeyOpenMove>().SetIsOpen();
    }
    public void OnTriggerInteract()
    {
        Open();
    }
    public bool GetCanInteract()
    {
        return canIntract;
    }
    public void ResetTakarabako()
    {
        if(canIntract == false)
        {
            animator.SetTrigger("Reset");
            canIntract = true;
            instanceKey = Instantiate(key, transform.position + offset, transform.rotation);
        }
    }
}
