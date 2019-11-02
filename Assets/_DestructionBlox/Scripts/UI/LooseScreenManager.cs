using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using DG.Tweening;

public class LooseScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            {
                CloseAdsBtn();
                //fin de la possibilité de regarder une pub
            }

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
        //1 ouvrir le Timer avec une rotation
        //2 1s après faire apparaitre le non merci
        //faire descendre pendant 5s le timer
        //faire apparaitre le essaye encore

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
    public GameManager gameManager;
    public GameObject adsObj;
    public ProceduralImage filedImageTimer;
    public GameObject noThanks;
    public GameObject tryAgain;
    public Button btnRetry;
    public bool activeTimerAds;
    public float timerAds;
}
