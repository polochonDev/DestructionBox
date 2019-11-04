using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerAmmunitions : MonoBehaviour
{
    void Start()
    {
        if (currentAmmo == null)
        {
            currentAmmo = new Stack<InfoAmmo>();
        }
    }

    public void CleanAmmo()
    {
        if (currentAmmo != null)
            currentAmmo.Clear();
    }
    public void UpdateInterfaceAmmo()
    {
        var listAmmo = currentAmmo.ToArray();
        if (nextAmmoSprite != null)
        {
            if (listAmmo.Length > 0)
            {
                nextAmmoSprite.color = new Color(255, 255, 255, 1);

                var typeOfNextBalls = listAmmo[0].myType;
                if (typeOfNextBalls != ProjectileMove.TypeOfBall.Color)
                    nextAmmoSprite.sprite = infoAmmoSprite.Find((x) => typeOfNextBalls == x.myType).mySprite;
                else
                {
                    var colorOfNextBalls = listAmmo[0].myColor;
                    nextAmmoSprite.sprite = infoAmmoSprite.Find((x) => colorOfNextBalls == x.myColor).mySprite;
                }
            }
            else
            {
                nextAmmoSprite.sprite = null;
                nextAmmoSprite.color = new Color(0, 0, 0, 0);
            }
        }
        gameManager.uiManager.nbBalls.text = listAmmo.Length.ToString();
    }
    public Stack<InfoAmmo> GenerateAmmunitions(int size)
    {
        if (currentAmmo == null)
        {
            currentAmmo = new Stack<InfoAmmo>();
        }

        for (int i = 0; i < size; i++)
        {
            int random = Random.Range(0, 101);
            InfoAmmo ammo = new InfoAmmo();
            if (random >= 0 && random <= 80)
            {
                ammo = ModelAmmo.Find((x) => x.myType == ProjectileMove.TypeOfBall.Color);
                int randomColor = Random.Range(0, Enum.GetNames(typeof(ElementDestructible.Color)).Length - 1);
                ammo.myColor = (ElementDestructible.Color)randomColor;
            }
            else if (random > 80 && random <= 100)
                ammo = ModelAmmo.Find((x) => x.myType == ProjectileMove.TypeOfBall.Explosive);

            currentAmmo.Push(ammo);
        }
        UpdateInterfaceAmmo();
        return currentAmmo;
    }

    [System.Serializable]
    public struct InfoAmmo
    {
        public ProjectileMove.TypeOfBall myType;
        public ElementDestructibleColor.Color myColor;
        public float speedAmmo;
        public GameObject prefabAmmo;
    }
    public Stack<InfoAmmo> currentAmmo;

    [SerializeField]
    private List<InfoAmmo> ModelAmmo;
    [SerializeField]
    private List<InfoAmmoSprite> infoAmmoSprite;
    [SerializeField]
    private Image nextAmmoSprite;
    [SerializeField]
    private GameManager gameManager;
    [System.Serializable]
    private class InfoAmmoSprite
    {
        public ProjectileMove.TypeOfBall myType;
        public ElementDestructibleColor.Color myColor;
        public Sprite mySprite;
    }
}
