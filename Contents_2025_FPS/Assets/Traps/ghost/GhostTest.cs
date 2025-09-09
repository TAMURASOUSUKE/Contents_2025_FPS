using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTest : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    const int DAMAGE = 60;
    float angle = 0;
    float posY;

    Vector3 offset = new Vector3(0, 1.3f, 0);
    void Start()
    {
        posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        GhostMove();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerController>().TakeDamage(DAMAGE);

        Destroy(this.gameObject);
    }

    void GhostMove()
    {
        Vector3 targetPos = player.transform.position + offset;
        Vector3 dir = (targetPos - transform.position).normalized;

        angle += Time.deltaTime;
       
        posY = Mathf.Sin(angle) * 0.007f;

        Debug.Log(angle);

        Vector3 moveVec = dir * 0.01f;
        Vector3 moveUpDown = new Vector3(0f, posY, 0f);
        transform.position += moveVec;
        transform.position += moveUpDown;

        transform.LookAt(targetPos);
    }
}


