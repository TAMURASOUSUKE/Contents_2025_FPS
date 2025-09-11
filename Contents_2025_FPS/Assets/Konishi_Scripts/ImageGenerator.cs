using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGenerator : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] CharacterManager characterManager;
    float imageTimer;
    float imageCreateTime = 2f;
    float minSize = 0;  
    float maxSize = 0;
    float createTime = 0;
    public float xPosMax;
    public float xPosMin;
    public float yPosMax;
    public float yPosMin;
    public float inDuration = 1f;
    public float outDuration = 0.5f;
    public Canvas canvas;
    public GameObject[] image;
    private List<GameObject> spawnedImages = new List<GameObject>(); // 生成したものをリストで管理する

    void Start()
    {
    }

    void Update()
    {
        ImageCreate();
    }

    void ImageCreate()
    {
        if (characterManager.GetIsRange())
        {
            imageTimer += Time.deltaTime;
            int index = Random.Range(0, image.Length);
            float xPos = Random.Range(xPosMin, xPosMax);
            float yPos = Random.Range(yPosMin, yPosMax);
            GameObject prefab = image[index];
            if (imageTimer > imageCreateTime)
            {
                
                GameObject newImage = Instantiate(prefab, parent.transform);    //生成
                RectTransform rt = newImage.GetComponent<RectTransform>();

                Vector2 vec2 = new Vector2(xPos, yPos);     //生成位置のランダム化
                rt.anchoredPosition = vec2;

                //float angle = Random.Range(0f, 360f);       //生成向きのランダム化
                //rt.rotation = Quaternion.Euler(0, 0, angle);

                float scale = Random.Range(minSize, maxSize);       //大きさのランダム化
                rt.localScale = new Vector2(scale, scale);

                Image img = newImage.GetComponent<Image>();
                StartCoroutine(FadeIn(img, inDuration));
               

                spawnedImages.Add(newImage);            //生成したものをリストに追加

                imageTimer = 0;
            }
        }        //else
        //{
        //    StartCoroutine(FadeOut(outDuration));
        //}

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

        yield return new WaitForSeconds(createTime); // しばらく表示してから消す時間を調整（必要に応じて変える）
        StartCoroutine(FadeOutSingle(img, outDuration));
    }

    IEnumerator FadeOutSingle(Image img, float duration)
    {
            if (image != null)
            {
                Color c = img.color;     //現在の色(Alpha値)を取る
                float alphaTime = 0f;
                float startAlpha = c.a;             //現在のAlpha値を保存
                while (alphaTime < duration)
                {
                    alphaTime += Time.deltaTime;
                    float t = alphaTime / duration;
                    c.a = Mathf.Lerp(startAlpha, 0f, t);
                    img.color = c;
                    yield return null;
                }
                c.a = 0f;
                img.color = c;
                Destroy(img);
        }
    }

    public void SetImageCreateTime(float time)
    {
        imageCreateTime = time;
    }

    public void SetCreateTime(float time)
    {
        createTime = time;
    }

    public void SetSize(float min, float max)
    {
        minSize = min;
        maxSize = max;
    }
}
