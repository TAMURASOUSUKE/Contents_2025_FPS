using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject red;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject green;
    [SerializeField] GameObject nomal;
    [SerializeField] GameObject redGene;
    [SerializeField] GameObject blueGene;
    [SerializeField] GameObject greenGene;
    [SerializeField] GameObject nomalGene;
    [SerializeField] ColorManager colorManager;
    List<GameObject> enemys = new List<GameObject>();

    public bool isGeneratedR = false;
    public bool isGeneratedG = false;
    public bool isGeneratedB = false;
    public bool isGeneratedN = false;

    bool canGenerateR = false;
    bool canGenerateG = false;
    bool canGenerateB = false;
    bool canGenerateN = false;

    public enum EnemyType
    {
        RedEnemy,
        BlueEnemy,
        WhiteEnemy,
        GreenEnemy
    }

    private void Update()
    {
        Generate();
        VisibleEnemy();
    }

    void Generate()
    {
        if (canGenerateR)
        {
            Instantiate(red, redGene.transform.position, Quaternion.identity);
            canGenerateR = false;
        }
        if (canGenerateG)
        {
            Instantiate(blue, greenGene.transform.position, Quaternion.identity);
            canGenerateG = false;
        }
        if (canGenerateB)
        {
            Instantiate(green, blueGene.transform.position, Quaternion.identity);
            canGenerateB = false;
        }
        if (canGenerateN)
        {
            Instantiate(nomal, nomalGene.transform.position, Quaternion.identity);
            canGenerateN = false;
        }
    }

    void VisibleEnemy()
    {
        foreach (GameObject enemy in enemys)
        {
            GhostTest enemyscript = enemy.GetComponent<GhostTest>();
            if(colorManager.IsCurrentColorR() && enemyscript.GetEnemyType() == EnemyType.RedEnemy)
            {
                enemy.SetActive(false);
            }
            else if (colorManager.IsCurrentColorG() && enemyscript.GetEnemyType() == EnemyType.GreenEnemy)
            {
                enemy.SetActive(false);
            }
            else if (colorManager.IsCurrentColorB() && enemyscript.GetEnemyType() == EnemyType.BlueEnemy)
            {
                enemy.SetActive(false);
            }
            else
            {
                enemy.SetActive(true);
            }
        }
    }


    public void SetGenerateFlagR(bool isGenerate)
    {
        canGenerateR = isGenerate;
    }

    public void SetGenerateFlagG(bool isGenerate)
    {
        canGenerateG = isGenerate;
    }

    public void SetGenerateFlagB(bool isGenerate)
    {
        canGenerateB = isGenerate;
    }

    public void SetGenerateFlagN(bool isGenerate)
    {
        canGenerateN = isGenerate;
    }


    public void AddEnemy(GameObject obj)
    {
        enemys.Add(obj);
    } 

    public void RemoveEnemy(GameObject obj)
    {
        enemys.Remove(obj);
    }
}
