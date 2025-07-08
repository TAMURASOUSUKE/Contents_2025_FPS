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
        // Volumeコンポーネントを取得
        volume = GetComponent<Volume>();

        // VolumeProfile から ColorAdjustments を取得
        if (volume.profile.TryGet<ColorAdjustments>(out var color))
        {
            colorAdjustments = color;
        }
        else
        {
            Debug.LogWarning("ColorAdjustments が VolumeProfile に設定されていません！");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (colorAdjustments != null)
            {
                // 例：赤い色フィルターを設定
                colorAdjustments.colorFilter.value = new Color(1f, 0f, 0f, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (colorAdjustments != null)
            {
                // 例：緑色フィルターを設定
                colorAdjustments.colorFilter.value = new Color(0f, 1f, 0f, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (colorAdjustments != null)
            {
                // 例：青色フィルターを設定
                colorAdjustments.colorFilter.value = new Color(0f, 0f, 1f, 1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (colorAdjustments != null)
            {
                // 例：元の色に戻す
                colorAdjustments.colorFilter.value = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
