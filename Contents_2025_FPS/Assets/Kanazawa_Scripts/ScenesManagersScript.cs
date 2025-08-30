using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagersScripts : MonoBehaviour
{
    bool isGoal = false;
    [SerializeField] PlayerController playerController;
    int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // フレームレートを固定
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = playerController.GetHp();
        if(currentHP <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           Debug.Log("Goal");
           SceneManager.LoadScene("GoalScene");
        }

    }
 
}
