using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.GetInt("Level");
        InitLevel();
        uiManager.SetVibration();
    }
    public void InitLevel(bool reset = false)
    {
        playerAmmunitions.transform.position = startPos.position;
        playerAmmunitions.transform.eulerAngles = startPos.eulerAngles;
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
        uiManager.lastBalls.SetActive(false);

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
        currentScore = 0;
        currentGameState = GameState.InGame;

        ScoreUi.text = currentScore.ToString();
        StartCoroutine("EnablePhysicSmooth");
        int nbBalls = 3 + (int)(currentLevel * 0.2f);
        nbBalls = nbBalls < 6 ? nbBalls : 6;
        AddBalls(nbBalls);
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
        var ldModel = SelectLd();
        var generateLd = new GameObject(ldModel.name);
        generateLd.transform.position = new Vector3(0, 0, 0);
        ldModel.transform.position = new Vector3(0, 0, 0);

        int luckNormal = (int)(currentLevel * 0.9f);
        luckNormal = luckNormal <= 10 ? luckNormal : 25;

        int luckExplosive = (int)(currentLevel * .3f);
        luckExplosive = luckExplosive <= 10 ? luckNormal : 10;

        int luckBonus = (int)(currentLevel * 0.5f);
        luckBonus = luckBonus <= 10 ? luckBonus : 10;

        int luckColor = 100 - luckBonus - luckExplosive;

        for (int i = 0; i < ldModel.transform.childCount; i++)
        {
            int random = UnityEngine.Random.Range(0, 101);
            ElementDestructible.TypeOfElement typeOfBox = ElementDestructible.TypeOfElement.Normal;
            if (random <= luckNormal)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Normal;
                GameObject elementNormal = Instantiate(elementBasePrefab, ldModel.transform.GetChild(i).transform.position, ldModel.transform.GetChild(i).transform.rotation);
                elementNormal.GetComponent<ElementDestructible>().InitElement(this, typeOfBox);
                elementNormal.transform.parent = generateLd.transform;
                currentLD.Add(elementNormal);
            }
            else if (random > luckNormal && random <= luckColor)
            {
                typeOfBox = ElementDestructible.TypeOfElement.Color;
                GameObject elementColor = Instantiate(elementColorPrefab, ldModel.transform.GetChild(i).transform.position, ldModel.transform.GetChild(i).transform.rotation);
                elementColor.GetComponent<ElementDestructibleColor>().InitElement(this, typeOfBox);
                elementColor.transform.parent = generateLd.transform;
                currentLD.Add(elementColor);
                targetScore += 10;

            }
            else if (random > luckColor && random <= (luckColor+luckBonus))
            {
                typeOfBox = ElementDestructible.TypeOfElement.Bonus;
                GameObject elementBonus = Instantiate(elementBonusPrefab, ldModel.transform.GetChild(i).transform.position, ldModel.transform.GetChild(i).transform.rotation);
                elementBonus.GetComponent<ElementDestructibleBonus>().InitElement(this, typeOfBox);
                elementBonus.transform.parent = generateLd.transform;
                currentLD.Add(elementBonus);
                targetScore += 100;
            }
            else if (random > (luckColor + luckBonus) && random <= (luckColor + luckBonus + luckExplosive))
            {
                typeOfBox = ElementDestructible.TypeOfElement.Explosive;
                GameObject elementBombe = Instantiate(elementExplosivePrefab, ldModel.transform.GetChild(i).transform.position, ldModel.transform.GetChild(i).transform.rotation);
                elementBombe.GetComponent<ElementDestructibleBombe>().InitElement(this, typeOfBox);
                elementBombe.transform.parent = generateLd.transform;
                currentLD.Add(elementBombe);
            }
            yield return new WaitForSeconds(0.05f);
        }
        int resultTotal = (luckColor + luckBonus + luckExplosive);
        float multiplicatorScore = (0.5f + (currentLevel * 0.02f));
        multiplicatorScore = multiplicatorScore <= 1.05f ? multiplicatorScore : 1.05f;

        targetScore = (int)(targetScore * (multiplicatorScore));

        uiManager.startBtn.interactable = true;
        Destroy(ldModel);
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

    public void WinLevel()
    {
        if (currentGameState == GameState.EndGame)
            return;
        uiManager.startBtn.interactable = false;
        if (currentGameState == GameState.CheckEnd)
            uiManager.timerBeforeLoose.transform.parent.DOScale(0, 0.5f).OnComplete(() => uiManager.timerBeforeLoose.transform.parent.gameObject.SetActive(false));

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
        currentGameState = GameState.CheckEnd;
        StartCoroutine("CheckLoose");
    }
    IEnumerator CheckLoose()
    {
        float count = 5;
        uiManager.timerBeforeLoose.transform.parent.gameObject.SetActive(true);
        uiManager.timerBeforeLoose.transform.parent.localScale = Vector3.zero;
        uiManager.timerBeforeLoose.transform.parent.DOScale(1, 0.5f);
        while (count > 0)
        {
            uiManager.timerBeforeLoose.fillAmount = count / 5f;
            count -= Time.deltaTime;
            yield return null;
        }
        uiManager.timerBeforeLoose.transform.parent.DOScale(0, 0.5f).OnComplete(()=> uiManager.timerBeforeLoose.transform.parent.gameObject.SetActive(false));

        if (currentGameState == GameState.CheckEnd)
        {
            uiManager.startBtn.interactable = false;

            currentGameState = GameState.EndGame;
            uiManager.looseInterface.SetActive(true);
            uiManager.inGameCanvas.SetActive(false);
            spawnProjectile.enabled = false;
            spawnProjectile.transform.GetChild(0).gameObject.SetActive(false);
            uiManager.looseInterface.GetComponent<LooseScreenManager>().InitScreen();
        }
        yield return null;
    }
    public void AddScore(int sc)
    {
        StartCoroutine("ScoreSmooth", sc);
    }
    public IEnumerator ScoreSmooth(int sc)
    {
        int currentAdd = 0;
        while (currentAdd < sc)
        {
            currentScore += 1;
            currentAdd += 1;
            ScoreUi.text = currentScore.ToString();
            ScoreUi.GetComponent<Animation>().Play();
            uiManager.UpdateScore(currentScore, targetScore);
            yield return new WaitForSeconds(0.01f);
        }
        if (currentScore >= targetScore)
            WinLevel();
        yield return null;
    }
    public bool GetVibrationActivate()
    {
        return vibrationActivate;
    }
    public void InverseVibrationActivate()
    {
        vibrationActivate = !vibrationActivate;
    }

    public UIManager uiManager;

    [SerializeField]
    private PlayerAmmunitions playerAmmunitions;
    [SerializeField]
    private SpawnProjectile spawnProjectile;
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    public GameObject elementExplosivePrefab;
    [SerializeField]
    public GameObject elementBonusPrefab;
    [SerializeField]
    public GameObject elementColorPrefab;
    [SerializeField]
    public GameObject elementBasePrefab;

    [SerializeField]
    public List<GameObject > currentLD;
    [SerializeField]
    private List<GameObject> Lds;

    [SerializeField]
    private TextMeshProUGUI ScoreUi;
    [SerializeField]
    private Transform groundGraph;
    [SerializeField]
    private Transform startPos;
    private int currentLevel;
    private int currentScore;

    private int targetScore;
    private bool vibrationActivate;
    private GameState currentGameState;

    private enum GameState
    {
        Start,
        InGame,
        CheckEnd,
        EndGame
    }
}
