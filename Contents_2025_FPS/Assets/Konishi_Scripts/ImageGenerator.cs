using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGenerator : MonoBehaviour
{
    public int imageMax;
    float imageTimer;
    float imageCreateTime = 2f;
    public float xPosMax;
    public float xPosMin;
    public float yPosMax;
    public float yPosMin;
    public Canvas canvas;
    public GameObject[] image;
    ColorManager colorManager;
    private List<GameObject> spawnedImages = new List<GameObject>(); // 生成したものをリストで管理する

    void Start()
    {
        colorManager = FindObjectOfType<ColorManager>();
    }

    void Update()
    {
        ImageCreate();
    }

    void ImageCreate()
    {
        if (colorManager.isColorChange)
        {
            imageTimer += Time.deltaTime;
            if (imageTimer > imageCreateTime)
            {
                int index = Random.Range(0, image.Length);
                float xPos = Random.Range(xPosMin, xPosMax);
                float yPos = Random.Range(yPosMin, yPosMax);
                GameObject prefab = image[index];
                GameObject newImage = Instantiate(prefab, canvas.transform);
                RectTransform rt = newImage.GetComponent<RectTransform>();

                Vector2 vec2 = new Vector2(xPos, yPos);     //生成位置のランダム化
                rt.anchoredPosition = vec2;

                float angle = Random.Range(0f, 360f);       //生成向きのランダム化
                rt.rotation = Quaternion.Euler(0, 0, angle);

                float scale = Random.Range(0.5f, 1f);       //大きさのランダム化
                rt.localScale = new Vector2(scale, scale);

                Image img = newImage.GetComponent<Image>();
                StartCoroutine(FadeIn(img, 1f));

                spawnedImages.Add(newImage);            //生成したものをリストに追加

                imageTimer = 0;
            }
        }
        else
        {
            MakeAllImagesTransparent();
        }
    }
    IEnumerator FadeIn(Image img, float duration)
    {
        Color c = img.color;
        c.a = 0f; // 透明にしてからスタート
        img.color = c;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            c.a = Mathf.Lerp(0f, 1f, t);
            img.color = c;
            yield return null; // 次のフレームまで待つ
        }

        c.a = 1f;
        img.color = c;
    }

    void MakeAllImagesTransparent()
    {
        foreach (GameObject img in spawnedImages)
        {
            Image imageComponent = img.GetComponent<Image>();
            Color c = imageComponent.color;
            c.a = 0f;
            imageComponent.color = c;
        }
    }
}
