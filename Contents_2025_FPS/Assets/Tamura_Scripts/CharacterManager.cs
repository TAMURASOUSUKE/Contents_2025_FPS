using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float distance;
    public List<GameObject> enemys = new List<GameObject>();
    float range;
    float minRange = 0f;
    bool isRange = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRange();
        Debug.Log(isRange);
      
    }

    void CalculateRange()
    {

        if (enemys.Count == 0) return;

        // 最小の長さを求める
        minRange = (enemys[0].transform.position - player.transform.position).sqrMagnitude * -1f;
        for (int i = 1; i < enemys.Count; i++)
        {
            range = (enemys[i].transform.position - player.transform.position).sqrMagnitude * -1f;
            if (minRange > range)
            {
                minRange = range;
            }
        }

        if (distance * distance > minRange)
        {
            isRange = true;
        }
        else
        {
            isRange = false;
        }
    }


    public bool GetIsRange()
    {
        return isRange;
    }
}
