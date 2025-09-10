using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] EnemyManager.EnemyType enemyType;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] KeyManager keyManager;


    // Update is called once per frame
    void Update()
    {


        if (enemyType == EnemyManager.EnemyType.RedEnemy && keyManager.useRedKey && !enemyManager.isGeneratedR)
        {
            enemyManager.SetGenerateFlagR(true);
            enemyManager.isGeneratedR = true;
        }

        if (enemyType == EnemyManager.EnemyType.BlueEnemy && keyManager.useBlueKey && !enemyManager.isGeneratedB)
        {
            enemyManager.SetGenerateFlagB(true);
            enemyManager.isGeneratedB = true;
        }

        if (enemyType == EnemyManager.EnemyType.GreenEnemy && keyManager.useGreenKey && !enemyManager.isGeneratedG)
        {
            enemyManager.SetGenerateFlagG(true);
            enemyManager.isGeneratedG = true;
        }

        if (enemyType == EnemyManager.EnemyType.WhiteEnemy && !enemyManager.isGeneratedN)
        {
            enemyManager.SetGenerateFlagN(true);
            enemyManager.isGeneratedN = true;
        }
    }
}
