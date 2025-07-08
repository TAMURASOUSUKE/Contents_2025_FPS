using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorManager : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private bool isColor = false;

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
            Debug.LogWarning("ColorAdjustments �� VolumeProfile �ɐݒ肳��Ă��܂���I");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (colorAdjustments != null)
            {
                // ��F�Ԃ��F�t�B���^�[��ݒ�
                colorAdjustments.colorFilter.value = new Color(1f, 0f, 0f, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (colorAdjustments != null)
            {
                // ��F�ΐF�t�B���^�[��ݒ�
                colorAdjustments.colorFilter.value = new Color(0f, 1f, 0f, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (colorAdjustments != null)
            {
                // ��F�F�t�B���^�[��ݒ�
                colorAdjustments.colorFilter.value = new Color(0f, 0f, 1f, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (colorAdjustments != null)
            {
                // ��F���̐F�ɖ߂�
                colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
