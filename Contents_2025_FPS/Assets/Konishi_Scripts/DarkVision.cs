using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DarkVision : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;

    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        vignette.intensity.value = 0.4f;
    }

}
