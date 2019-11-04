using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using DG.Tweening;

public class LooseScreenManager : MonoBehaviour
{
    void Update()
    {
        if (activeTimerAds)
        {
            if (timerAds >= 0)
            {
                timerAds -= Time.deltaTime;
                filedImageTimer.fillAmount = (timerAds * 0.1f) * 2;
            }
            else
                CloseAdsBtn();
        }
    }

    public void CloseAdsBtn()
    {
        activeTimerAds = false;
        tryAgain.SetActive(true);
        adsObj.SetActive(false);
        btnRetry.interactable = true;
    }

    public void ClickAds()
    {
        gameManager.AddBalls(3);
    }

    public void Retry()
    {
        gameManager.InitLevel(true);
    }

    public void InitScreen()
    {
        noThanks.SetActive(false);
        tryAgain.SetActive(false);
        btnRetry.interactable = false;
        adsObj.SetActive(true);
        activeTimerAds = false;
        timerAds = 5;
        filedImageTimer.fillAmount = 1;
        adsObj.transform.localScale = Vector3.zero;

        adsObj.transform.DOScale(1, 1).OnComplete(()=>adsObj.transform.DOScale(1,0.2f));
        adsObj.transform.eulerAngles = new Vector3(0, 0, 180);
        adsObj.transform.DORotate(Vector3.zero, 1f).OnComplete(() => noThanks.SetActive(true));
        activeTimerAds = true;
    }

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject adsObj;
    [SerializeField]
    private ProceduralImage filedImageTimer;
    [SerializeField]
    private GameObject noThanks;
    [SerializeField]
    private GameObject tryAgain;
    [SerializeField]
    private Button btnRetry;
    private bool activeTimerAds;
    private float timerAds;
}
