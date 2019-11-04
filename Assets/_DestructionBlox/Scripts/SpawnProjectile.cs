using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public void Start()
    {
        dragDistance = Screen.width / 15;
    }
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonUp(0))
        {
            FireProjectile();
        }
#elif UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstTouchPos = touch.position;
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastTouchPos = touch.position;
                if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > dragDistance || Mathf.Abs(lastTouchPos.y - firstTouchPos.y) > dragDistance)
                {
                    //drag
                }
                else
                {
                    FireProjectile();
                }
            }
        }
#endif
    }
    public void CreateProjectile()
    {
        if (ammunitions.currentAmmo.Count == 1)
        {
            gameManager.uiManager.lastBalls.SetActive(true);
            gameManager.uiManager.lastBalls.GetComponent<Animation>().Play();
        }
        if (ammunitions.currentAmmo.Count > 0)
        {
            if(currentProjectile != null)
                Destroy(currentProjectile.gameObject);
            currentInfoAmmo = ammunitions.currentAmmo.Pop();
            currentProjectile = Instantiate(currentInfoAmmo.prefabAmmo, firePoint.transform.position, Quaternion.identity);
            currentProjectile.GetComponent<ProjectileMove>().Init(this);
            currentProjectile.transform.parent = firePoint.transform;
            ballReady = true;
        }
        else
        {
            gameManager.LooseLevel();
        }
        ammunitions.UpdateInterfaceAmmo();

    }
    private void FireProjectile()
    {
        if (!ballReady)
            return;
        currentProjectile.transform.parent = null;

        if (lookTouchTarget != null)
            currentProjectile.transform.localRotation = lookTouchTarget.GetRotation();
        currentProjectile.GetComponent<Rigidbody>().velocity = currentProjectile.transform.forward * currentInfoAmmo.speedAmmo;
        ballReady = false;
    }
    public PlayerAmmunitions.InfoAmmo GetInfoCurrentAmmo()
    {
        return currentInfoAmmo;
    }

    public GameManager gameManager;

    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private PlayerAmmunitions ammunitions;
    [SerializeField]
    private LookTouchTarget lookTouchTarget;

    private GameObject currentProjectile;
    private PlayerAmmunitions.InfoAmmo currentInfoAmmo;

    private Vector3 firstTouchPos;   //First touch position
    private Vector3 lastTouchPos;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private bool ballReady;
}
