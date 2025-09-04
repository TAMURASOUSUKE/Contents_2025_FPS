using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DarkVision : MonoBehaviour
{
    float currentHp;                //プレイヤーの現在のHP
    float maxHp;                    //プレイヤの最大HP
    float currentValue;             //現在の視界の暗さ
    public float maxValue = 0.5f;   //視界の濃さの最大
    private Volume volume;
    private Vignette vignette;
    PlayerController Player;

    void Start()
    {
        Player = GetComponentInParent<PlayerController>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        maxHp = (float)Player.GetHp();
    }
    void Update()
    {
        currentHp = (float)Player.GetHp();
        currentValue = (1f - currentHp / maxHp) * maxValue;
        vignette.intensity.value = currentValue;
    }
}
