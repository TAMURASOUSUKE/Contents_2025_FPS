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
    float redTimer = 0;         //�ԃt�B���^�[�̐؂�ւ��N�[���^�C��
    float greenTimer = 0;       //�΃t�B���^�[�̐؂�ւ��N�[���^�C��
    float blueTimer = 0;        //�t�B���^�[�̐؂�ւ��N�[���^�C��
    float timer = 0;            //�b�J�E���^�[
    float filterCoolTime = 7;   //�t�B���^�[�؂�ւ��̃N�[���^�C��
    float colorTiemr = 3;       //�t�B���^�[�̗L������
    bool isColorChange = false; //�t�B���^�[���L����
    bool isRedTimer = false;
    bool isGreenTimer = false;
    bool isBlueTimer = false;


    void Start()
    {
        // Volume�R���|�[�l���g���擾
        volume = GetComponent<Volume>();
        cubeMesh = cube.GetComponent<MeshRenderer>();
        // VolumeProfile ���� ColorAdjustments ���擾
        if (volume.profile.TryGet<ColorAdjustments>(out var color))
        {
            colorAdjustments = color;
        }
        else
        {
            Debug.Log("ColorAdjustments �� VolumeProfile �ɐݒ肳��Ă��܂���I");
        }
    }

    void Update()
    {
        SelectColor();  //�t�B���^�[�ύX
        Timer();        //�N�[���^�C���֘A
    }


    
    void SelectColor()  //�J���[�ύX�̑匳  Update�Ŏg��
    {
        bool isRed = Input.GetKeyDown(KeyCode.Alpha1);
        bool isGreen = Input.GetKeyDown(KeyCode.Alpha2);
        bool isBule = Input.GetKeyDown(KeyCode.Alpha3);
        if (!isColorChange)
        {
            if (isRed)   //1�L�[
            {
                if (!isRedTimer)
                {
                    if (colorAdjustments != null)
                    {
                        RedColor();             //�ԃt�B���^�[
                        isColorChange = true;   //�t�B���^�[���L���ɂȂ���
                        isRedTimer = true;      //�ԃt�B���^�[�̃N�[���^�C��
                       cubeMesh.enabled = false;
                    }
                }
            }
            if (isGreen)   //2�L�[
            {
                if (!isGreenTimer)
                {
                    if (colorAdjustments != null)
                    {
                        GreenColor();           //�΃t�B���^�[
                        isColorChange = true;
                        isGreenTimer = true;
                    }
                }
            }
            if (isBule)   //3�L�[
            {
                if (!isBlueTimer)
                {
                    if (colorAdjustments != null)
                    {
                        BlueColor();            //�t�B���^�[
                        isColorChange = true;
                        isBlueTimer = true;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))   //4�L�[
            {
                if (colorAdjustments != null)
                {
                   DefaultColor();
                }
            }
        }
    }

    void RedColor() //��
    {
        //�Ԃ��F�t�B���^�[��ݒ�
        colorAdjustments.colorFilter.value = new Color(1f, 0.6f, 0.6f, 1f);
    }
    void GreenColor()//��
    {
        // ��F�ΐF�t�B���^�[��ݒ�
        colorAdjustments.colorFilter.value = new Color(0.6f, 1f, 0.6f, 1f);
    }
    void BlueColor() //��
    {
        //�F�t�B���^�[��ݒ�
        colorAdjustments.colorFilter.value = new Color(0.6f, 0.6f, 1f, 1f);
    }
    void DefaultColor()//�f�t�H���g�̐F
    {
        // ��F���̐F�ɖ߂�
        colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f);
        isColorChange = false;
    }
    void Timer()    //�N�[���^�C���֘A Update�Ŏg��
    {
        if (isColorChange)      //�t�B���^�[���L���ɂȂ����Ƃ�
        {
            ChangeTimer();      //�t�B���^�[�L������
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
    void RedTimer() //�N�[���^�C���̐ݒ�
    {
        
        redTimer += Time.deltaTime;      //�b�J�E���g
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
    void ChangeTimer()  //�t�B���^�[�̗L������
    {
        timer += Time.deltaTime;    //�b�J�E���g
        if (timer >= colorTiemr)     //�L������
        {
            colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f); // ��F���̐F�ɖ߂�
            timer = 0;
            isColorChange = false;
            cubeMesh.enabled = true;
        }
    }
}
