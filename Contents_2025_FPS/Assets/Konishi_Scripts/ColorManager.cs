using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/*
 if elseでtrueをfalseにしてfalseをtrueにして透明化させる
*/
public class ColorManager : MonoBehaviour
{
    [SerializeField] GameObject otosiana;
    [SerializeField] GameObject torabasami;
    MeshRenderer cubeMesh;
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    float timer = 0;            //秒カウンターフィルターの有効時間で使う
    float redTimer = 0;      //各フィルターのタイム計測に使用   
    float greenTimer = 0;       
    float blueTimer = 0;        
    public float filterCoolTime = 7;   //フィルター切り替えのクールタイム
    public float colorTiemr = 3;       //フィルターの有効時間
    bool isColorChange = false; //フィルターが有効か
    bool isRedTimer = false;
    bool isGreenTimer = false;
    bool isBlueTimer = false;
    MeshRenderer meshRender;
    BoxCollider boxCollider;
    GameObject[] redVisibles;
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

        redVisibles = GameObject.FindGameObjectsWithTag("Red_Visible");    //findはstartの中で
        redHiddens = GameObject.FindGameObjectsWithTag("Red_Hidden");    
        redColliderOnrys = GameObject.FindGameObjectsWithTag("Red_ColliderOnry");
        greenVisibles = GameObject.FindGameObjectsWithTag("Green_Visible");
        greenHiddens = GameObject.FindGameObjectsWithTag("Green_Hidden");
        greenColliderOnrys = GameObject.FindGameObjectsWithTag("Green_ColliderOnry");
        blueVisibles = GameObject.FindGameObjectsWithTag("Blue_Visible");
        blueHiddens = GameObject.FindGameObjectsWithTag("Blue_Hidden");
        blueColliderOnrys = GameObject.FindGameObjectsWithTag("Blue_ColliderOnry");
        foreach (GameObject red in redHiddens)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            BoxCollider bc = red.GetComponent<BoxCollider>();
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
        //meshRender = GetComponent<MeshRenderer>();
        //boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        SelectColor();  //フィルター変更
        Timer();        //クールタイム関連
    }


    
    void SelectColor()  //カラー変更の大元  Updateで使う
    {
        bool isRed = Input.GetKeyDown(KeyCode.Alpha1);
        bool isGreen = Input.GetKeyDown(KeyCode.Alpha2);
        bool isBlue = Input.GetKeyDown(KeyCode.Alpha3);
        if (!isColorChange)
        {
            if (isRed)   //1キー
            {
                //Debug.Log("赤");
                if (!isRedTimer)
                {
                    //Debug.Log("timer");
                    //if (colorAdjustments != null)
                    //{
                        //Debug.Log("フィルター起動");
                        RedColor();             //赤フィルター
                        isColorChange = true;   //フィルターが有効になった
                        isRedTimer = true;      //赤フィルターのクールタイム
                       //cubeMesh.enabled = false;
                    //}
                }
            }
            if (isGreen)   //2キー
            {
                if (!isGreenTimer)
                {
                    GreenColor();           //緑フィルター
                    isColorChange = true;
                    isGreenTimer = true;
                }
            }
            if (isBlue)   //3キー
            {
                if (!isBlueTimer)
                {
                    BlueColor();            //青フィルター
                    isColorChange = true;
                    isBlueTimer = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))   //4キー
            {
                DefaultColor();
            }
        }
    }
    
    void RedColor() //赤
    {
        //赤い色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(1f, 0.6f, 0.6f, 1f);
        foreach (GameObject redVisible in redVisibles)
        {
            redVisible.SetActive(false);
            //Debug.Log(redObject);
        }
        foreach (GameObject redHidden in redHiddens)
        {
            MeshRenderer mr = redHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = redHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
        foreach (GameObject red in redColliderOnrys)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    void GreenColor()//緑
    {
        //緑色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 1f, 0.6f, 1f);
        foreach (GameObject greenObject in greenVisibles)
        {
            greenObject.SetActive(false);
        }
        foreach (GameObject greenHidden in greenHiddens)
        {
            MeshRenderer mr = greenHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = greenHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
        foreach (GameObject green in greenColliderOnrys)
        {
            MeshRenderer mr = green.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    void BlueColor() //青
    {
        //青色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 0.6f, 1f, 1f);
        foreach (GameObject blueObject in blueVisibles)
        {
            blueObject.SetActive(false);
        }
        foreach (GameObject blueHidden in blueHiddens)
        {
            MeshRenderer mr = blueHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = blueHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
        foreach (GameObject blue in blueColliderOnrys)
        {
            MeshRenderer mr = blue.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    void DefaultColor()//デフォルトの色
    {
        // 例：元の色に戻す
        colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f);
        isColorChange = false;
    }
    void Timer()    //クールタイム関連 Updateで使う
    {
        if (isColorChange)      //フィルターが有効になったとき
        {
            ChangeTimer();      //フィルター有効時間
        }
        if (isRedTimer)
        {
            RedTimer();
        }
        if (isBlueTimer)
        {
            BlueTimer();
        }
        if (isGreenTimer)
        {
            GreenTimer();
        }
    }
    void RedTimer() //クールタイムの設定
    {
        
        redTimer += Time.deltaTime;      //秒カウント
        if (redTimer > filterCoolTime)
        {
            redTimer = 0;
            isRedTimer = false;
        }
    }
    void GreenTimer()
    {
        greenTimer += Time.deltaTime;
        if (greenTimer > filterCoolTime)
        {
            greenTimer = 0;
            isGreenTimer = false;
        }
    }
    void BlueTimer()
    {
        blueTimer += Time.deltaTime;
        if (blueTimer > filterCoolTime)
        {
            blueTimer = 0;
            isBlueTimer = false;
        }
    }
    void ChangeTimer()  //フィルターの有効時間
    {
        timer += Time.deltaTime;    //秒カウント
        if (timer >= colorTiemr)     //有効時間
        {
            colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f); // 例：元の色に戻す
            timer = 0;
            isColorChange = false;
            //cubeMesh.enabled = true;
            //赤
            foreach (GameObject redObject in redVisibles)
            {
                redObject.SetActive(true);
            }
            foreach (GameObject redHidden in redHiddens)
            {
                MeshRenderer mr = redHidden.GetComponent<MeshRenderer>();
                BoxCollider bc = redHidden.GetComponent<BoxCollider>();
                mr.enabled = false;
                bc.enabled = false;
            }
            foreach (GameObject red in redColliderOnrys)
            {
                MeshRenderer mr = red.GetComponent<MeshRenderer>();
                mr.enabled = false;
            }
            //緑
            foreach (GameObject greenObject in greenVisibles)
            {
                greenObject.SetActive(true);
            }
            foreach (GameObject greenHidden in greenHiddens)
            {
                MeshRenderer mr = greenHidden.GetComponent<MeshRenderer>();
                BoxCollider bc = greenHidden.GetComponent<BoxCollider>();
                mr.enabled = false;
                bc.enabled = false;
            }
            foreach (GameObject green in greenColliderOnrys)
            {
                MeshRenderer mr = green.GetComponent<MeshRenderer>();
                mr.enabled = false;
            }
            //青
            foreach (GameObject blueObject in blueVisibles)
            {
                blueObject.SetActive(true);
            }
            foreach (GameObject blueHidden in blueHiddens)
            {
                MeshRenderer mr = blueHidden.GetComponent<MeshRenderer>();
                BoxCollider bc = blueHidden.GetComponent<BoxCollider>();
                mr.enabled = false;
                bc.enabled = false;
            }
            foreach (GameObject blue in blueColliderOnrys)
            {
                MeshRenderer mr = blue.GetComponent<MeshRenderer>();
                mr.enabled = false;
            }
        }
    }
}