using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/*
 if elseでtrueをfalseにしてfalseをtrueにして透明化させる
*/
public class ColorManager : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    public float filterCoolTime = 4f;    //フィルター切り替えのクールタイム
    float timer;
    bool isColorChange = false;         //フィルターが有効か
    bool canFilterChange = true;        //フィルターに切り替え可能か
    GameObject[] redVisibles;           //それぞれのオブジェクトを構造体で取得
    GameObject[] redHiddens;
    GameObject[] redColliderOnrys;
    GameObject[] greenVisibles;
    GameObject[] greenHiddens;
    GameObject[] greenColliderOnrys;
    GameObject[] blueVisibles;
    GameObject[] blueHiddens;
    GameObject[] blueColliderOnrys;
    
    void Start()
    {
        // Volumeコンポーネントを取得
        volume = GetComponent<Volume>();
        //cubeMesh = otosiana.GetComponent<MeshRenderer>();
        // VolumeProfile から ColorAdjustments を取得
        if (volume.profile.TryGet<ColorAdjustments>(out var color))
        {
            colorAdjustments = color;
        }
        else
        {
            Debug.Log("ColorAdjustments が VolumeProfile に設定されていません！");
        }

        //構造体をtagで識別
        redVisibles = GameObject.FindGameObjectsWithTag("Red_Visible");    //findはstartの中で
        redHiddens = GameObject.FindGameObjectsWithTag("Red_Hidden");    
        redColliderOnrys = GameObject.FindGameObjectsWithTag("Red_ColliderOnry");
        greenVisibles = GameObject.FindGameObjectsWithTag("Green_Visible");
        greenHiddens = GameObject.FindGameObjectsWithTag("Green_Hidden");
        greenColliderOnrys = GameObject.FindGameObjectsWithTag("Green_ColliderOnry");
        blueVisibles = GameObject.FindGameObjectsWithTag("Blue_Visible");
        blueHiddens = GameObject.FindGameObjectsWithTag("Blue_Hidden");
        blueColliderOnrys = GameObject.FindGameObjectsWithTag("Blue_ColliderOnry");
        //消したいところの削除,初期化
        foreach (GameObject red in redHiddens)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>(); //見た目
            BoxCollider bc = red.GetComponent<BoxCollider>();   //collider
            mr.enabled = false;
            bc.enabled = false;
        }
        foreach (GameObject red in greenHiddens)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            BoxCollider bc = red.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
        foreach (GameObject red in blueHiddens)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            BoxCollider bc = red.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
        foreach (GameObject red in redColliderOnrys)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
        foreach (GameObject green in greenColliderOnrys)
        {
            MeshRenderer mr = green.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
        foreach (GameObject blue in blueColliderOnrys)
        {
            MeshRenderer mr = blue.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
    }

    void Update()
    {
        SelectColor();  //フィルター変更
        Timer();
        Debug.Log(canFilterChange);
    }
    
    void SelectColor()  //カラー変更の大元  Updateで使う
    {
        bool isRed = Input.GetKeyDown(KeyCode.Alpha1);
        bool isGreen = Input.GetKeyDown(KeyCode.Alpha2);
        bool isBlue = Input.GetKeyDown(KeyCode.Alpha3);
        if (canFilterChange)
        {
            if (isRed)   //1キー
            {
                RedColor();             //赤フィルター
            }
            if (isGreen)   //2キー
            {
                GreenColor();           //緑フィルター
            }
            if (isBlue)   //3キー
            {
                BlueColor();            //青フィルター
            }
        }
        if (isColorChange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))   //4キー
            {
                DefaultColor();
            }
        }
    }
    //--------------赤--------------
    void RedColor() 
    {
        //赤い色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(1f, 0.6f, 0.6f, 1f);
        isColorChange = true;
        //Visible
        foreach (GameObject redVisible in redVisibles)
        {
            redVisible.SetActive(false);
        }
        //Hidden
        foreach (GameObject redHidden in redHiddens)
        {
            MeshRenderer mr = redHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = redHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
        //ColliderOnry
        foreach (GameObject red in redColliderOnrys)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    //--------------緑--------------
    void GreenColor()
    {
        //緑色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 1f, 0.6f, 1f);
        isColorChange = true;
        //Visible
        foreach (GameObject greenObject in greenVisibles)
        {
            greenObject.SetActive(false);
        }
        //Hidden
        foreach (GameObject greenHidden in greenHiddens)
        {
            MeshRenderer mr = greenHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = greenHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
        //ColliderOnry
        foreach (GameObject green in greenColliderOnrys)
        {
            MeshRenderer mr = green.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    //--------------青--------------
    void BlueColor() 
    {
        //青色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 0.6f, 1f, 1f);
        isColorChange = true;
        //Visible
        foreach (GameObject blueObject in blueVisibles)
        {
            blueObject.SetActive(false);
        }
        //Hidden
        foreach (GameObject blueHidden in blueHiddens)
        {
            MeshRenderer mr = blueHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = blueHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
        //ColliderOnry
        foreach (GameObject blue in blueColliderOnrys)
        {
            MeshRenderer mr = blue.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    void DefaultColor()//デフォルトの色
    {
        NormalColor();
    }
    void NormalColor()
    {
        colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f); // 例：元の色に戻す
        isColorChange = false;
        canFilterChange = false;
        //--------------赤--------------
        //Visible
        foreach (GameObject redObject in redVisibles)
        {
            redObject.SetActive(true);
        }
        //Hidden
        foreach (GameObject redHidden in redHiddens)
        {
            MeshRenderer mr = redHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = redHidden.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
        //ColliderOnry
        foreach (GameObject red in redColliderOnrys)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
        //--------------緑--------------
        //Visible
        foreach (GameObject greenObject in greenVisibles)
        {
            greenObject.SetActive(true);
        }
        //Hidden
        foreach (GameObject greenHidden in greenHiddens)
        {
            MeshRenderer mr = greenHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = greenHidden.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
        //ColliderOnry
        foreach (GameObject green in greenColliderOnrys)
        {
            MeshRenderer mr = green.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
        //--------------青--------------
        //Visible
        foreach (GameObject blueObject in blueVisibles)
        {
            blueObject.SetActive(true);
        }
        //Hidden
        foreach (GameObject blueHidden in blueHiddens)
        {
            MeshRenderer mr = blueHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = blueHidden.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
        //ColliderOnry
        foreach (GameObject blue in blueColliderOnrys)
        {
            MeshRenderer mr = blue.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
    }
    void Timer()
    {
        if (!canFilterChange)
        {
            FilterCoolTime();
        }
    }
    void FilterCoolTime()
    {
        timer += Time.deltaTime;
        if (timer >= filterCoolTime)
        {
            timer = 0;
            canFilterChange = true;
        }
    }
}