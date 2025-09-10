using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    [SerializeField] CharacterManager characterManager;
    GhostTest redScr;
    GhostTest greenScr;
    GhostTest blueScr;
    GhostTest nomalScr;

    public bool isGeneratedR = false;
    public bool isGeneratedG = false;
    public bool isGeneratedB = false;
    public bool isGeneratedN = false;

    bool canGenerateR = false;
    bool canGenerateG = false;
    bool canGenerateB = false;
    bool canGenerateN = false;

    private void Start()
    {
        redScr = red.GetComponent<GhostTest>();
        greenScr = green.GetComponent<GhostTest>();
        blueScr = blue.GetComponent<GhostTest>();
        nomalScr = nomal.GetComponent<GhostTest>();
        redScr.enabled = false;
        greenScr.enabled = false;
        blueScr.enabled = false;
        nomalScr.enabled = false;
    }

    private void Update()
    {
        Generate();
    }

    void Generate()
    {
        if (canGenerateR)
        {
            red.transform.position = new Vector3(redGene.transform.position.x, redGene.transform.position.y, redGene.transform.position.z);
            redScr.isHit = false;
            canGenerateR = false;
            redScr.enabled = true;
            characterManager.enemys.Add(red);
        }
        if (canGenerateG)
        {
            green.transform.position = new Vector3(greenGene.transform.position.x, greenGene.transform.position.y, greenGene.transform.position.z);
            greenScr.isHit = false;
            canGenerateG = false;
            greenScr.enabled = true;
            characterManager.enemys.Add(green);
        }
        if (canGenerateB)
        {
            blue.transform.position = new Vector3(blueGene.transform.position.x, blueGene.transform.position.y, blueGene.transform.position.z);
            blueScr.isHit = false;
            canGenerateB = false;
            blueScr.enabled = true;
            characterManager.enemys.Add(blue);
        }
        if (canGenerateN)
        {
            nomal.transform.position = new Vector3(nomalGene.transform.position.x, nomalGene.transform.position.y, nomalGene.transform.position.z);
            nomalScr.isHit = false;
            canGenerateN = false;
            nomalScr.enabled = true;
            characterManager.enemys.Add(nomal);
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
