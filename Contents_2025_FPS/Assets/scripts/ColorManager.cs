using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorManager : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    float timer = 0;
    float redTimer = 0;
    float greenTimer = 0;
    float blueTimer = 0;
    bool isColorChange = true;

    void Start()
    {
        // Volume�R���|�[�l���g���擾
        volume = GetComponent<Volume>();

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
        SelectColor();
        Timer();
    }


    void Timer()
    {
        if (isColorChange)
        {
            ChangeTimer();
        }
    }
    void SelectColor()
    {
        if (isColorChange)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (colorAdjustments != null)
                {
                    RedColor();
                    isColorChange = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (colorAdjustments != null)
                {
                    GreenColor();
                    isColorChange = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (colorAdjustments != null)
                {
                    BlueColor();
                    isColorChange = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (colorAdjustments != null)
                {
                   DefaultColor();
                }
            }
        }
    }

    void RedColor()
    {
        //�Ԃ��F�t�B���^�[��ݒ�
        colorAdjustments.colorFilter.value = new Color(1f, 0.6f, 0.6f, 1f);
    }
    void GreenColor()
    {
        // ��F�ΐF�t�B���^�[��ݒ�
        colorAdjustments.colorFilter.value = new Color(0.6f, 1f, 0.6f, 1f);
    }
    void BlueColor() 
    {
        //�F�t�B���^�[��ݒ�
        colorAdjustments.colorFilter.value = new Color(0.6f, 0.6f, 1f, 1f);
    }
    void DefaultColor()
    {
        // ��F���̐F�ɖ߂�
        colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f);
    }
    void ChangeTimer()
    {
        timer += Time.deltaTime;
    }
}
