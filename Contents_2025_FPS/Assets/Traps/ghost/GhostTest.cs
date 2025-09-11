using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTest : MonoBehaviour
{

    GameObject player;
    GameObject enemyManagerObj;
    EnemyManager enemyManager;
    ScenesManagersScripts scenesManagers;
    CharacterManager generateCharacter;
    Vector3 initPos;
    const int DAMAGE = 100;
    float angle = 0;
    float posY;
    public bool isHit = false;

    Vector3 offset = new Vector3(0, 1.3f, 0);
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyManagerObj = GameObject.Find("Enemy");
        scenesManagers = GameObject.Find("SceneManager").GetComponent<ScenesManagersScripts>();
        generateCharacter = GameObject.Find("GenerateCharacter").GetComponent<CharacterManager>();
        enemyManager = enemyManagerObj.GetComponent<EnemyManager>();
        posY = transform.position.y;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (scenesManagers.currentScene == ScenesManagersScripts.Scene.GAME)
            GhostMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerController>().TakeDamage(DAMAGE);
        transform .position = initPos;

        // generateCharacter.enemys.Remove(this.gameObject);
        // isHit = true;
        // Destroy(this.gameObject);
    }

    void GhostMove()
    {
        Vector3 targetPos = player.transform.position + offset;
        Vector3 dir = (targetPos - transform.position).normalized;

        angle += Time.deltaTime;
       
        posY = Mathf.Sin(angle) * 0.007f;

        Vector3 moveVec = dir * 0.01f;
        Vector3 moveUpDown = new Vector3(0f, posY, 0f);
        transform.position += moveVec;
        transform.position += moveUpDown;

        transform.LookAt(targetPos);
    }

}


