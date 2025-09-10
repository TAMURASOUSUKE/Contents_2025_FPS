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
}
