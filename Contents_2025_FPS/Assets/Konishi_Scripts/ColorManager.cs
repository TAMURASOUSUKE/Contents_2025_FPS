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
    private ChromaticAberration chromaticAberration;
    private DepthOfField depthOfField;
    const int DAMAGE = 1;               //使用時のダメージ
    public float filterCoolTime = 2.5f; //切り替えのクールタイム
    float timer;                        //クールタイムの時間計測
    float hitTimer;                     //ダメージを食らうまでのタイマー
    float damageTimer;                  //ダメージを食らった時に使うタイマー
    float efectTimer;                   //エフェクトに使うタイマー
    float chromaticEfect;               //フィルター使用時間に応じて視界エフェクトの追加
    float focalEfect;                   //フィルター使用時間に応じて視界エフェクトの追加
    float apertureEfect;                //フィルター使用時間に応じて視界エフェクトの追加
    float depthSpeed;                   //ぼかすスピードを計算して入れる
    float apertureSpeed;                //↑と同じ
    float depthTimer = 5f;              //ぼかしがMAXになるまでの時間
    float startDepth = 5f;              //フィルターが起動してからDepthが起動するまでの時間
    public float maxChromatic = 1;      //各エフェクトの最大値
    public float maxFocal = 80f;
    public float maxAperture = 25f;
    float chromaticDuration = 5f;       //chromaticが最大になるまでの時間
    public bool isColorChange = false;  //フィルターが有効か
    public bool canFilterChange = true;        //フィルターに切り替え可能か
    public bool isRed = false;
    public bool isGreen = false;
    public bool isBlue = false;
    bool isCurrentR = false;
    bool isCurrentG = false;
    bool isCurrentB = false;
    public bool removeFilter = false;
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
        //volume.profile.TryGet<ColorAdjustments>(out var color);
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out depthOfField);

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
        SelectColor();      //フィルター変更
        Timer();            //フィルターのクールタイム
        UseDamage();        //フィルター使用時間に応じてダメージ
        ColorDamageEfect(); //フィルター長時間使用に応じて視界不良
    }
    
    void SelectColor()  //カラー変更の大元  Updateで使う
    {
        isRed = Input.GetKeyDown(KeyCode.Alpha1);  //1キー
        isGreen = Input.GetKeyDown(KeyCode.Alpha2);//2キー
        isBlue = Input.GetKeyDown(KeyCode.Alpha3); //3キー
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
        removeFilter = false;
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
        removeFilter = false;
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
        removeFilter = false;
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
        removeFilter = true;

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
                removeFilter = false;
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

    //---------------カラー長時間使用時の視界エフェクト---------------
    void ColorDamageEfect()
    {
        if (isColorChange)
        {
            efectTimer += Time.deltaTime;
            float t = efectTimer / chromaticDuration;
            chromaticEfect = Mathf.Lerp(0f, maxChromatic, t);
            if (efectTimer > startDepth)
            {
                depthSpeed = maxFocal / depthTimer;
                apertureSpeed = maxAperture / depthTimer;
                focalEfect = Mathf.MoveTowards(focalEfect, maxFocal, depthSpeed * Time.deltaTime);
                apertureEfect = Mathf.MoveTowards(apertureEfect, maxAperture, apertureSpeed * Time.deltaTime);
            }
        }
        else
        {
            chromaticEfect = 0f;
            focalEfect = 0f;
            apertureEfect = 0f;
            efectTimer = 0f;
        }
        chromaticAberration.intensity.value = chromaticEfect;
        depthOfField.focalLength.value = focalEfect;
        depthOfField.aperture.value = apertureEfect;
    }

    //-----------------------フィルターのON,OFF-----------------------

    //------------赤------------
    void OnRedVisible() 
    {
        foreach (GameObject redVisible in redVisibles)
        {
            redVisible.SetActive(false);
        }
        isCurrentR = true;
    }
    void OffRedVisible()
    {
        foreach (GameObject redObject in redVisibles)
        {
            redObject.SetActive(true);
        }
        isCurrentR = false;
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
        isCurrentG = true;
    }
    void OffGreenVisible()
    {
        foreach (GameObject greenObject in greenVisibles)
        {
            greenObject.SetActive(true);
        }
        isCurrentG = false;
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
        isCurrentB = true;
    }
    void OffBlueVisible()
    {
        foreach (GameObject blueObject in blueVisibles)
        {
            blueObject.SetActive(true);
        }
        isCurrentB = false;
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



    public bool IsCurrentColorR()
    {
        return isCurrentR;
    }

    public bool IsCurrentColorG()
    {
        return isCurrentG;
    }

    public bool IsCurrentColorB()
    {
        return isCurrentB;
    }
    public void ResetColorManager()
    {
        NormalColor();
        chromaticEfect = 0f;
        focalEfect = 0f;
        apertureEfect = 0f;
        efectTimer = 0f;
        timer = 0;
        canFilterChange = true;
    }
}