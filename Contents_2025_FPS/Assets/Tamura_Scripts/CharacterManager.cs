using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] ImageGenerator imageGenerator;
    [SerializeField] float nearDistance;
    [SerializeField] float middleDistance;
    [SerializeField] float farDistance;
    [Header("表示し終わってから消すまでの時間")]
    [SerializeField] float nearCreateTime;
    [SerializeField] float middleCreateTime;
    [SerializeField] float farCreateTime;
    [Header("次の文字を表示するまでの時間")]
    [SerializeField] float nearImageCreateTime;
    [SerializeField] float middleImageCreateTime; 
    [SerializeField] float farImageCreateTime;
    [Header("文字のサイズ")]
    [SerializeField] float nearMinSize;
    [SerializeField] float nearMaxSize;
    [SerializeField] float middleMinSize;
    [SerializeField] float middleMaxSize;
    [SerializeField] float farMinSize;
    [SerializeField] float farMaxSize;
    
    public List<GameObject> enemys = new List<GameObject>();
    float range;
    float minRange = 0f;
    bool isRange = false;
    void Start()
    {
        
    }
    void Update()
    {
        CalculateRange();
    }

    void CalculateRange()
    {

        if (enemys.Count == 0) return;
        minRange = (enemys[0].transform.position - player.transform.position).magnitude;
        for (int i = 1; i < enemys.Count; i++)
        {
            range = (enemys[i].transform.position - player.transform.position).magnitude;
            if (minRange > range)
            {
                minRange = range;
            }
        }




        // 近い → 中くらい → 遠い の順に、距離 < 閾値で判定
        if (minRange < nearDistance)
        {
            Debug.Log("近くにいるよ！");
            imageGenerator.SetCreateTime(nearCreateTime);
            imageGenerator.SetImageCreateTime(nearImageCreateTime);
            imageGenerator.SetSize(nearMinSize, nearMaxSize);
            isRange = true;
        }
        else if (minRange < middleDistance)
        {
            Debug.Log("中くらいだよ！");
            imageGenerator.SetCreateTime(middleCreateTime);
            imageGenerator.SetImageCreateTime(middleImageCreateTime);
            imageGenerator.SetSize(middleMinSize, middleMaxSize);
            isRange = true;
        }
        else if (minRange < farDistance)
        {
            Debug.Log("遠いよ！");
            imageGenerator.SetCreateTime(farCreateTime);
            imageGenerator.SetImageCreateTime(farImageCreateTime);
            imageGenerator.SetSize(farMinSize, farMaxSize);
            isRange = true;
        }
        else
        {
            Debug.Log("完全に離れてるよ");
            isRange = false;
        }
    }


    public bool GetIsRange()
    {
        return isRange;
    }
}
