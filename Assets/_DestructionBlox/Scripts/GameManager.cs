using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        InitLevel();
    }
    public void InitLevel(bool reset = false)
    {
        uiManager.currentLevel.text = currentLevel.ToString();
        uiManager.nextLevel.text = (currentLevel + 1).ToString();
        currentGameState = GameState.Start;
        uiManager.fireworkVictory.SetActive(false);
        for (int i = 0; i< currentLD.Count; i++)
        {
            if (currentLD[i] != null)
                currentLD[i].GetComponent<ElementDestructible>().DestroyMeWithDelay(i * 0.01f);
        }
        currentLD.Clear();
        uiManager.startInterface.SetActive(false);

        uiManager.looseInterface.SetActive(false);
        uiManager.victoryInterface.SetActive(false);
        currentScore = 0;
        targetScore = 0;
        uiManager.UpdateScore(currentScore, 100);
        if (reset)
            StartCoroutine("GenerateLevelLdSmoothWithDelay", 1);
        else
            StartCoroutine("GenerateLevelLdSmooth");

        spawnProjectile.enabled = false;
        spawnProjectile.transform.GetChild(0).gameObject.SetActive(false);
        playerAmmunitions.CleanAmmo();
        StartCoroutine("CinematiqueIntro");
    }
    IEnumerator CinematiqueIntro()
    {
        cameraController.StartCoroutine("Make180");
        groundGraph.transform.localPosition = Vector3.up * -2;
        groundGraph.DOLocalMoveY(0, 0.7f);
        uiManager.startInterface.SetActive(true);

        yield return null;
    }
    public void StartLevel()
    {
        Debug.LogError("StartLevel");

        currentScore = 0;
        currentGameState = GameState.InGame;

        ScoreUi.text = currentScore.ToString();
        StartCoroutine("EnablePhysicSmooth");
        AddBalls(10);
        spawnProjectile.CreateProjectile();
        uiManager.startInterface.SetActive(false);
        uiManager.inGameCanvas.SetActive(true);
        spawnProjectile.enabled = true;
        spawnProjectile.transform.GetChild(0).gameObject.SetActive(true);

    }
    private IEnumerator GenerateLevelLdSmoothWithDelay(float timer)
    {
        yield return new WaitForSeconds(timer);
        yield return GenerateLevelLdSmooth();
    }
    private IEnumerator GenerateLevelLdSmooth()
    {
        var go = SelectLd();
        var GenerateLd = new GameObject();
        GenerateLd.name = go.name;
        GenerateLd.transform.position = new Vector3(0, 0, 0);
        go.transform.position = new Vector3(0, 0, 0);
        int luckNormal = (int)(currentLevel * 0.9f);//5
        luckNormal = luckNormal <= 10 ? luckNormal : 25;
        int luckExplosive = (int)(currentLevel * .9f);//5
        luckExplosive = luckExplosive <= 15 ? luckNormal : 15;
        int luckBonus = (int)(currentLevel * 1f);//5
        luckBonus = luckBonus <= 15 ? luckBonus : 15;
        int luckColor = 100 - luckBonus - luckExplosive;//90
        for (int i = 0; i < go.transform.childCount; i++)
        {
            int random = UnityEngine.Random.Range(0, 101);
            ElementDestructible.TypeOfElement typeOfBox = ElementDestructible.TypeOfElement.Normal;
            if (random <= luckNormal)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Normal;
                GameObject elementNormal = Instantiate(elementBasePrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                if (elementNormal.GetComponent<ElementDestructible>()!= null)
                elementNormal.GetComponent<ElementDestructible>().InitElement(this, typeOfBox);
                elementNormal.transform.parent = GenerateLd.transform;
                currentLD.Add(elementNormal);
            }
            if (random > luckNormal && random <= luckColor)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Color;
                GameObject elementColor = Instantiate(elementColorPrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                elementColor.GetComponent<ElementDestructibleColor>().InitElement(this, typeOfBox);
                elementColor.transform.parent = GenerateLd.transform;
                currentLD.Add(elementColor);
                targetScore += 10;

            }
            if (random > luckColor && random <= (luckColor+luckBonus))
            {
                typeOfBox = ElementDestructible.TypeOfElement.Bonus;
                GameObject elementBonus = Instantiate(elementBonusPrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                elementBonus.GetComponent<ElementDestructibleBonus>().InitElement(this, typeOfBox);
                elementBonus.transform.parent = GenerateLd.transform;
                currentLD.Add(elementBonus);
                targetScore += 100;
            }
            if (random > (luckColor + luckBonus) && random <= (luckColor + luckBonus + luckExplosive))
            {
                typeOfBox = ElementDestructible.TypeOfElement.Explosive;
                GameObject elementBombe = Instantiate(elementExplosivePrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                elementBombe.GetComponent<ElementDestructibleBombe>().InitElement(this, typeOfBox);
                elementBombe.transform.parent = GenerateLd.transform;
                currentLD.Add(elementBombe);
            }
            //  go.transform.GetChild(i).GetComponent<ElementDestructible>().InitElement(this, typeOfBox);
            go.transform.GetChild(i).name = i.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        int resultTotal = (luckColor + luckBonus + luckExplosive);
        Debug.Log(targetScore  + " " + resultTotal);
        float multiplicatorScore = (0.5f + (currentLevel * 0.02f));
        multiplicatorScore = multiplicatorScore <= 1.2f ? multiplicatorScore : 1.2f;
        targetScore = (int)(targetScore * (multiplicatorScore));

        yield return null;
    }
    public void AddBalls(int nb)
    {
        playerAmmunitions.GenerateAmmunitions(nb);
        if (currentGameState == GameState.EndGame)
        {
            uiManager.looseInterface.SetActive(false);
            uiManager.inGameCanvas.SetActive(true);
            uiManager.lastBalls.SetActive(false);
            Invoke("ActiveCanon", 0.5f);
        }
    }
    private void ActiveCanon()
    {
        spawnProjectile.enabled = true;
        spawnProjectile.transform.GetChild(0).gameObject.SetActive(true);
        spawnProjectile.CreateProjectile();

    }
    private IEnumerator EnablePhysicSmooth()
    {
        for (int igo = 0; igo < currentLD.Count; igo++)
        {
            currentLD[igo].GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForEndOfFrame();

        }
        yield return null;

    }
    private GameObject SelectLd()
    {
        int ld = UnityEngine.Random.Range(0, Lds.Count);
        return Instantiate(Lds[ld]);
    }
    private void GenerateLevelWithLd()
    {
        var go = SelectLd();
        var GenerateLd = new GameObject();
        GenerateLd.name = go.name;
        GenerateLd.transform.position = new Vector3(0, 0, 0);
        go.transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < go.transform.childCount; i++)
        {
            int random = UnityEngine.Random.Range(0, 101);
            ElementDestructible.TypeOfElement typeOfBox = ElementDestructible.TypeOfElement.Normal;
            if (random <= 10)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Normal;
                GameObject elementNormal = Instantiate(elementBasePrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                elementNormal.GetComponent<ElementDestructible>().InitElement(this, typeOfBox);
                elementNormal.transform.parent = GenerateLd.transform;
            }
            if (random> 10 && random <= 70)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Color;
                GameObject elementColor = Instantiate(elementColorPrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                elementColor.GetComponent<ElementDestructibleColor>().InitElement(this, typeOfBox);
                elementColor.transform.parent = GenerateLd.transform;

            }
            if (random > 70 && random <= 85)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Bonus;
                GameObject elementBonus = Instantiate(elementBonusPrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                elementBonus.GetComponent<ElementDestructibleBonus>().InitElement(this, typeOfBox);
                elementBonus.transform.parent = GenerateLd.transform;

            }
            if (random > 85 && random <= 100)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Explosive;
                GameObject elementBombe = Instantiate(elementExplosivePrefab, go.transform.GetChild(i).transform.position, Quaternion.identity);
                elementBombe.GetComponent<ElementDestructibleBombe>().InitElement(this, typeOfBox);
                elementBombe.transform.parent = GenerateLd.transform;

            }
            //  go.transform.GetChild(i).GetComponent<ElementDestructible>().InitElement(this, typeOfBox);
            go.transform.GetChild(i).name = i.ToString();
        }
    }

    public void WinLevel()
    {
        if (currentGameState == GameState.EndGame)
            return;
        currentGameState = GameState.EndGame;
        uiManager.victoryInterface.SetActive(true);
        uiManager.inGameCanvas.SetActive(false);
        spawnProjectile.enabled = false;
        uiManager.fireworkVictory.SetActive(true);
        currentLevel += 1;
        PlayerPrefs.SetInt("Level", currentLevel);
        spawnProjectile.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void LooseLevel()
    {
        currentGameState = GameState.EndGame;
        uiManager.looseInterface.SetActive(true);
        uiManager.inGameCanvas.SetActive(false);
        spawnProjectile.enabled = false;
        spawnProjectile.transform.GetChild(0).gameObject.SetActive(false);
        uiManager.looseInterface.GetComponent<LooseScreenManager>().InitScreen();
    }
    public void AddScore(int sc)
    {
        currentScore += sc;
            ScoreUi.text = currentScore.ToString();
        uiManager.UpdateScore(currentScore, targetScore);
        if (currentScore >= targetScore)
        {
            WinLevel();

        }
    }
    public int currentLevel;
    public int currentScore;
    public PlayerAmmunitions playerAmmunitions;
    public SpawnProjectile spawnProjectile;
    public GameObject elementExplosivePrefab;
    public GameObject elementBonusPrefab;
    public GameObject elementColorPrefab;
    public GameObject elementBasePrefab;
    public List<ElementDestructible> elementDestructiblesPrefab;
    public List<GameObject > currentLD;
    public List<GameObject> Lds;
    public TextMeshProUGUI ScoreUi;
    public UIManager uiManager;
    public CameraController cameraController;
    public Transform groundGraph;
    public bool vibrationActivate;
    int targetScore;
    public enum GameState
    {
        Start,
        InGame,
        EndGame
    }
    public GameState currentGameState;
}
