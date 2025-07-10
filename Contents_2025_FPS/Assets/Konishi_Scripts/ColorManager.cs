using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorManager : MonoBehaviour
{
    [SerializeField] GameObject cube;
    MeshRenderer cubeMesh;
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    float redTimer = 0;         //赤フィルターの切り替えクールタイム
    float greenTimer = 0;       //緑フィルターの切り替えクールタイム
    float blueTimer = 0;        //青フィルターの切り替えクールタイム
    float timer = 0;            //秒カウンター
    float filterCoolTime = 7;   //フィルター切り替えのクールタイム
    float colorTiemr = 3;       //フィルターの有効時間
    bool isColorChange = false; //フィルターが有効か
    bool isRedTimer = false;
    bool isGreenTimer = false;
    bool isBlueTimer = false;


    void Start()
    {
        // Volumeコンポーネントを取得
        volume = GetComponent<Volume>();
        cubeMesh = cube.GetComponent<MeshRenderer>();
        // VolumeProfile から ColorAdjustments を取得
        if (volume.profile.TryGet<ColorAdjustments>(out var color))
        {
            colorAdjustments = color;
        }
        else
        {
            Debug.Log("ColorAdjustments が VolumeProfile に設定されていません！");
        }
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
        bool isBule = Input.GetKeyDown(KeyCode.Alpha3);
        if (!isColorChange)
        {
            if (isRed)   //1キー
            {
                if (!isRedTimer)
                {
                    if (colorAdjustments != null)
                    {
                        RedColor();             //赤フィルター
                        isColorChange = true;   //フィルターが有効になった
                        isRedTimer = true;      //赤フィルターのクールタイム
                       cubeMesh.enabled = false;
                    }
                }
            }
            if (isGreen)   //2キー
            {
                if (!isGreenTimer)
                {
                    if (colorAdjustments != null)
                    {
                        GreenColor();           //緑フィルター
                        isColorChange = true;
                        isGreenTimer = true;
                    }
                }
            }
            if (isBule)   //3キー
            {
                if (!isBlueTimer)
                {
                    if (colorAdjustments != null)
                    {
                        BlueColor();            //青フィルター
                        isColorChange = true;
                        isBlueTimer = true;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))   //4キー
            {
                if (colorAdjustments != null)
                {
                   DefaultColor();
                }
            }
        }
    }

    void RedColor() //赤
    {
        //赤い色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(1f, 0.6f, 0.6f, 1f);
    }
    void GreenColor()//緑
    {
        // 例：緑色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 1f, 0.6f, 1f);
    }
    void BlueColor() //青
    {
        //青色フィルターを設定
        colorAdjustments.colorFilter.value = new Color(0.6f, 0.6f, 1f, 1f);
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
            cubeMesh.enabled = true;
        }
    }
}
