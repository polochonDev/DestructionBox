﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public void UpdateScore(int currentScore, int targetScore)
    {
        sliderScore.value = ((float)currentScore / targetScore) ;
    }

    public void ClickOption()
    {
        if (optionOpen)
        {
            btnOption.Play("CloseOption");
            optionOpen = false;
        }
        else
        {
            btnOption.Play("OpenOption");
            optionOpen = true;
        }
    }

    public void OpenVictoryScreen()
    {
        victoryInterface.SetActive(true);
        inGameCanvas.SetActive(false);
        textVictory.localScale = Vector3.zero;
        textVictory.DOScale(1, 1f);
        fireworkVictory.SetActive(true);
    }

    public void SetVibration(GameObject not = null)
    {
        gm.InverseVibrationActivate();
        if (not != null)
            not.SetActive(!gm.GetVibrationActivate());
    }
    public GameManager gm;
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
    public Animation btnOption;
    public Button startBtn;
    public Image timerBeforeLoose;
    bool optionOpen;
}
