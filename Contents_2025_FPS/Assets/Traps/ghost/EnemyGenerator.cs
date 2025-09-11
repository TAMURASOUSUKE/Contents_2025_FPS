using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] GameManager gameManager;


    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetRedKey() && !enemyManager.isGeneratedR)
        {
            enemyManager.SetGenerateFlagR(true);
            enemyManager.isGeneratedR = true;
        }

        if (gameManager.GetBlueKey() && !enemyManager.isGeneratedB)
        {
            enemyManager.SetGenerateFlagB(true);
            enemyManager.isGeneratedB = true;
        }

        if (gameManager.GetGreenKey() && !enemyManager.isGeneratedG)
        {
            enemyManager.SetGenerateFlagG(true);
            enemyManager.isGeneratedG = true;
        }

        if (!enemyManager.isGeneratedN)
        {
            enemyManager.SetGenerateFlagN(true);
            enemyManager.isGeneratedN = true;
        }
    }
}
