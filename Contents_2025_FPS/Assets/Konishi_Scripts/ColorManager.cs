using System.Collections;
using System.Threading;
using Unity.VisualScripting;
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
    const int DAMAGE = 10;              //使用時のダメージ
    public float filterCoolTime = 4f;   //切り替えのクールタイム
    float timer;                        //クールタイムの時間計測
    float hitTimer;                     //ダメージを食らうまでのタイマー
    float damageTimer;                  //ダメージを食らった時に使うタイマー
    public bool isColorChange = false;         //フィルターが有効か
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
    PlayerController Player;

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

        Player = GetComponentInParent<PlayerController>();

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
            OffRedHidden();
        }
        foreach (GameObject red in greenHiddens)
        {
            OffGreenHidden();
        }
        foreach (GameObject red in blueHiddens)
        {
            OffBlueHidden();
        }
        foreach (GameObject red in redColliderOnrys)
        {
            OffRedColliderOnry();
        }
        foreach (GameObject green in greenColliderOnrys)
        {
            OffGreenColliderOnry();
        }
        foreach (GameObject blue in blueColliderOnrys)
        {
            OffBlueColliderOnry();
        }
    }

    void Update()
    {
        SelectColor();  //フィルター変更
        Timer();        //フィルターのクールタイム
        UseDamage();    //フィルター使用時間に応じてダメージ
    }
    
    void SelectColor()  //カラー変更の大元  Updateで使う
    {
        bool isRed = Input.GetKeyDown(KeyCode.Alpha1);  //1キー
        bool isGreen = Input.GetKeyDown(KeyCode.Alpha2);//2キー
        bool isBlue = Input.GetKeyDown(KeyCode.Alpha3); //3キー
        if (canFilterChange)
        {
            if (isRed)   
            {
                RedColor();             //赤フィルター
            }
            if (isGreen)   
            {
                GreenColor();           //緑フィルター
            }
            if (isBlue)   
            {
                BlueColor();            //青フィルター
            }
        }
        if (isColorChange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))       //4キー
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
        OnRedVisible();
        //Hidden
        OnRedHidden();
        //ColliderOnry
        OnRedColliderOnry();
        //それ以外の無効化
        OffGreenVisible();
        OffBlueVisible();
        OffGreenHidden();
        OffBlueHidden();
        OffGreenColliderOnry();
        OffBlueColliderOnry();
    }
    //--------------緑--------------
    void GreenColor()
    {
        //緑色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 1f, 0.6f, 1f);
        isColorChange = true;
        //Visible
        OnGreenVisible();
        //Hidden
        OnGreenHidden();
        //ColliderOnry
        OnGreenColliderOnry();
        //それ以外の無効化
        OffRedVisible();
        OffBlueVisible();
        OffRedHidden();
        OffBlueHidden();
        OffRedColliderOnry();
        OffBlueColliderOnry();
    }
    //--------------青--------------
    void BlueColor() 
    {
        //青色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 0.6f, 1f, 1f);
        isColorChange = true;
        //Visible
        OnBlueVisible();
        //Hidden
        OnBlueHidden();
        //ColliderOnry
        OnBlueColliderOnry();
        //それ以外の無効化
        OffRedVisible();
        OffGreenVisible();
        OffRedHidden();
        OffGreenHidden();
        OffRedColliderOnry();
        OffGreenColliderOnry();
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
        OffRedVisible();
        //Hidden
        OffRedHidden();
        //ColliderOnry
        OffRedColliderOnry();
        //--------------緑--------------
        //Visible
        OffGreenVisible();
        //Hidden
        OffGreenHidden();
        //ColliderOnry
        OffGreenColliderOnry();
        //--------------青--------------
        //Visible
        OffBlueVisible();
        //Hidden
        OffBlueHidden();
        //ColliderOnry
        OffBlueColliderOnry();
    }
    void Timer()
    {
        if (!canFilterChange)
        {
            timer += Time.deltaTime;
            if (timer >= filterCoolTime)
            {
                timer = 0;
                canFilterChange = true;
            }
        }
    }

    //---------------ダメージ関連---------------
    void UseDamage()
    {
        if (isColorChange)
        {
            float damageTime = 2f;
            float invincibleTime = 0.5f;
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageTime)
            {
                hitTimer += Time.deltaTime;
                if (hitTimer >= invincibleTime)
                {
                    Player.TakeDamage(DAMAGE);
                    hitTimer = 0;
                }
            }
        }
        else if (!isColorChange)
        {
            damageTimer = 0;
        }
    }

    //-----------------------フィルターのON,OFF-----------------------

    //------------赤------------
    void OnRedVisible()
    {
        foreach (GameObject redVisible in redVisibles)
        {
            redVisible.SetActive(false);
        }
    }
    void OffRedVisible()
    {
        foreach (GameObject redObject in redVisibles)
        {
            redObject.SetActive(true);
        }
    }
    void OnRedHidden()
    {
        foreach (GameObject redHidden in redHiddens)
        {
            MeshRenderer mr = redHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = redHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
    }
    void OffRedHidden()
    {
        foreach (GameObject redHidden in redHiddens)
        {
            MeshRenderer mr = redHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = redHidden.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
    }
    void OnRedColliderOnry()
    {
        foreach (GameObject red in redColliderOnrys)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    void OffRedColliderOnry()
    {
        foreach (GameObject red in redColliderOnrys)
        {
            MeshRenderer mr = red.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
    }

    //------------緑------------
    void OnGreenVisible()
    {
        foreach (GameObject greenObject in greenVisibles)
        {
            greenObject.SetActive(false);
        }
    }
    void OffGreenVisible()
    {
        foreach (GameObject greenObject in greenVisibles)
        {
            greenObject.SetActive(true);
        }
    }
    void OnGreenHidden()
    {
        foreach (GameObject greenHidden in greenHiddens)
        {
            MeshRenderer mr = greenHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = greenHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
    }
    void OffGreenHidden()
    {
        foreach (GameObject greenHidden in greenHiddens)
        {
            MeshRenderer mr = greenHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = greenHidden.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
    }
    void OnGreenColliderOnry()
    {
        foreach (GameObject green in greenColliderOnrys)
        {
            MeshRenderer mr = green.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    void OffGreenColliderOnry()
    {
        foreach (GameObject green in greenColliderOnrys)
        {
            MeshRenderer mr = green.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
    }

    //------------青------------
    void OnBlueVisible()
    {
        foreach (GameObject blueObject in blueVisibles)
        {
            blueObject.SetActive(false);
        }
    }
    void OffBlueVisible()
    {
        foreach (GameObject blueObject in blueVisibles)
        {
            blueObject.SetActive(true);
        }
    }
    void OnBlueHidden()
    {
        foreach (GameObject blueHidden in blueHiddens)
        {
            MeshRenderer mr = blueHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = blueHidden.GetComponent<BoxCollider>();
            mr.enabled = true;
            bc.enabled = true;
        }
    }
    void OffBlueHidden()
    {
        foreach (GameObject blueHidden in blueHiddens)
        {
            MeshRenderer mr = blueHidden.GetComponent<MeshRenderer>();
            BoxCollider bc = blueHidden.GetComponent<BoxCollider>();
            mr.enabled = false;
            bc.enabled = false;
        }
    }
    void OnBlueColliderOnry()
    {
        foreach (GameObject blue in blueColliderOnrys)
        {
            MeshRenderer mr = blue.GetComponent<MeshRenderer>();
            mr.enabled = true;
        }
    }
    void OffBlueColliderOnry()
    {
        foreach (GameObject blue in blueColliderOnrys)
        {
            MeshRenderer mr = blue.GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
    }
}