using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int currentScore, int targetScore)
    {
        Debug.Log(currentScore + " " + targetScore);
        sliderScore.value = ((float)currentScore / targetScore) ;
    }
    public void OpenVictoryScreen()
    {
        victoryInterface.SetActive(true);
        inGameCanvas.SetActive(false);
        textVictory.localScale = Vector3.zero;
        textVictory.DOScale(1, 1f);
        fireworkVictory.SetActive(true);

    }
    public GameObject inGameCanvas;
    public GameObject startInterface;
    public GameObject victoryInterface;
    public GameObject looseInterface;
    public TextMeshProUGUI nbBalls;
    public GameObject lastBalls;
    public Slider sliderScore;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI nextLevel;
    public GameObject fireworkVictory;
    public RectTransform textVictory;
}
