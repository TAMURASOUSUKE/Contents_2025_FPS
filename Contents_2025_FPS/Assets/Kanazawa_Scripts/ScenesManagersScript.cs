using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagersScripts : MonoBehaviour
{
    bool isGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // フレームレートを固定
    }

    // Update is called once per frame
    void Update()
    {
        
    }

       private void OnTriggerEnter(Collider other)
       {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Goal");
                SceneManager.LoadScene("GoalScene");
            }
            else
            {
            Debug.Log("NULL");
            }
       }
}
