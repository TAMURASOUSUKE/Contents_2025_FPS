using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTest : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    const int DAMAGE = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;

        Vector3 moveVec = dir * 0.01f;

        transform.position += moveVec;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerController>().TakeDamage(DAMAGE);

        Destroy(this.gameObject);
    }
}
