using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyosekiGenerator : MonoBehaviour
{
    public GameObject KyosekiPrefab;
    public float span1 = 0.6f;
    public float delta = 1.2f;

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span1)
        {

            GameObject go = Instantiate(KyosekiPrefab);

            go.transform.position = new Vector3(1,4,22);
            this.delta = 0;
        }
    }
}
