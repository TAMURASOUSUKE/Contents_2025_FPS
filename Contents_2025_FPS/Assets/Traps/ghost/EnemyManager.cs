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
    bool isHit = false;

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
        CheckIsHit();
    }

    void Generate()
    {
        if (canGenerateR)
        {
            red.transform.position = new Vector3(redGene.transform.position.x, redGene.transform.position.y, redGene.transform.position.z);
            redScr.SetIsHit(false);
            canGenerateR = false;
            redScr.enabled = true;
            characterManager.enemys.Add(red);
        }
        if (canGenerateG)
        {
            green.transform.position = new Vector3(greenGene.transform.position.x, greenGene.transform.position.y, greenGene.transform.position.z);
            greenScr.SetIsHit(false);
            canGenerateG = false;
            greenScr.enabled = true;
            characterManager.enemys.Add(green);
        }
        if (canGenerateB)
        {
            blue.transform.position = new Vector3(blueGene.transform.position.x, blueGene.transform.position.y, blueGene.transform.position.z);
            blueScr.SetIsHit(false);
            canGenerateB = false;
            blueScr.enabled = true;
            characterManager.enemys.Add(blue);
        }
        if (canGenerateN)
        {
            nomal.transform.position = new Vector3(nomalGene.transform.position.x, nomalGene.transform.position.y, nomalGene.transform.position.z);
            nomalScr.SetIsHit(false);
            canGenerateN = false;
            nomalScr.enabled = true;
            characterManager.enemys.Add(nomal);
        }
    }

    public void ResetGhost()
    {
        red.transform.position = redGene.transform.position;
        green.transform.position = greenGene.transform.position;
        blue.transform.position = blueGene.transform.position;
        nomal.transform.position = nomalGene.transform.position;
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

    public void CheckIsHit()
    {
        if (redScr.GetIsHit() || greenScr.GetIsHit() || blueScr.GetIsHit() || nomalScr.GetIsHit())
        {
            isHit = true;
        }
    }

    public bool ConsumeHit()
    {
        if (!isHit) return false;

        isHit = false;                 // 自分のフラグを落とす
        redScr.SetIsHit(false);        // 各ゴースト側も落としておく
        greenScr.SetIsHit(false);
        blueScr.SetIsHit(false);
        nomalScr.SetIsHit(false);
        return true;
    }

    public bool GetIsHit()
    {
        return isHit;
    }
}
