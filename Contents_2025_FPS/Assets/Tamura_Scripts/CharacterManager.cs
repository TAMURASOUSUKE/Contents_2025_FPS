using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] ImageGenerator imageGenerator;
    [SerializeField] float farDistance;
    [SerializeField] float middleDistance;
    [SerializeField] float nearDistance;
    
    public List<GameObject> enemys = new List<GameObject>();
    float range;
    float maxRange = 0f;
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
        maxRange = (enemys[0].transform.position - player.transform.position).sqrMagnitude;
        for (int i = 1; i < enemys.Count; i++)
        {
            range = (enemys[i].transform.position - player.transform.position).sqrMagnitude;
            if (maxRange < range)
            {
                maxRange = range;
            }
        }
       
        if (nearDistance * nearDistance < maxRange)
        {
            Debug.Log("近くにいるよ！");
            imageGenerator.SetCreateTime(0.3f);
            imageGenerator.SetImageCreateTime(0.3f);
            imageGenerator.SetSize(9f, 11f);
            isRange = true;
        }
        else if (middleDistance * middleDistance < maxRange)
        {
            Debug.Log("中くらいだよ！");
            imageGenerator.SetCreateTime(1f);
            imageGenerator.SetImageCreateTime(1f);
            imageGenerator.SetSize(6f, 7.5f);
            isRange = true;
        }
        else if (farDistance * farDistance < maxRange)
        {
            imageGenerator.SetCreateTime(1.3f);
            imageGenerator.SetImageCreateTime(1.3f);
            imageGenerator.SetSize(3f, 5f);
            Debug.Log("遠いよ！");
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
