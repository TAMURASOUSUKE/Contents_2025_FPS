using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorFilterEffect : MonoBehaviour
{
    public Material filterMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (filterMaterial != null)
        {
            Graphics.Blit(src, dest, filterMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
    