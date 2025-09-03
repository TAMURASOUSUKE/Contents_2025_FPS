using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DarkVision : MonoBehaviour
{
    int test1 = 70;
    int test2 = 100;
    float test3;
    float currentValue;    //åªç›ÇÃéãäEÇÃà√Ç≥
    private Volume volume;
    private Vignette vignette;
    PlayerController Player;

    void Start()
    {
        test3 = test1 / test2;
        Player = GetComponent<PlayerController>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        Debug.Log(test3);
    }
    void Update()
    {
        //Debug.Log(Player.GetHp());
        vignette.intensity.value = currentValue;
    }
}
