using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //currentProjectile = projectiles[0];
    }

    // Update is called once per frame
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
            Touch touch = Input.GetTouch(0); // get the touch
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
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
        Debug.LogError("CreateProjectile");

        if (ammunitions.currentAmmo.Count > 0)
        {
            if(currentProjectile != null)
                Destroy(currentProjectile.gameObject);
            currentInfoAmmo = ammunitions.currentAmmo.Pop();
            currentProjectile = Instantiate(currentInfoAmmo.prefabAmmo, firePoint.transform.position, Quaternion.identity);
            Debug.Log(firePoint.transform.position);
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
    public void FireProjectile()
    {
        if (!ballReady)
            return;
        currentProjectile.transform.parent = null;

        if (lookTouchTarget != null)
            currentProjectile.transform.localRotation = lookTouchTarget.GetRotation();
        currentProjectile.GetComponent<Rigidbody>().velocity = currentProjectile.transform.forward * currentInfoAmmo.speedAmmo;
        ballReady = false;
    }
    /*   private void CreateProjectile()
       {
     /*      if (ammunitions.currentAmmo.Count > 0)
           {
               GameObject projectile;
               if (firePoint != null)
               {
                   currentInfoAmmo = ammunitions.currentAmmo.Pop();
                   projectile = Instantiate(infoAmmo.prefabAmmo, firePoint.transform.position, Quaternion.identity);
                   if (lookTouchTarget != null)
                       projectile.transform.localRotation = lookTouchTarget.GetRotation();
                   projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * infoAmmo.speedAmmo;
               }
               else
                   Debug.Log("no fire point");
           }
           */
    //   }
    public GameManager gameManager;
    public GameObject firePoint;
    public PlayerAmmunitions ammunitions;
    public GameObject currentProjectile;
    public PlayerAmmunitions.InfoAmmo currentInfoAmmo;
    public LookTouchTarget lookTouchTarget;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private bool ballReady;
}
