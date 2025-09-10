using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GenerateCharacter : MonoBehaviour
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

        // ç≈è¨ÇÃí∑Ç≥ÇãÅÇﬂÇÈ
        minRange = (player.transform.position - enemys[0].transform.position).magnitude;
        for (int i = 1; i < enemys.Count; i++)
        {
            range = (player.transform.position - enemys[i].transform.position).magnitude;
            if (minRange > range)
            {
                minRange = range;
            }
        }

        if (distance > minRange)
        {
            isRange = true;
        }
        else
        {
            isRange = false;
        }
    }
}
