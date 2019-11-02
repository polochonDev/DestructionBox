using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileColor : ProjectileMove
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void DestroyMe()
    {
        sp.CreateProjectile();
        Debug.Log(myTypeOfBall.ToString() + this.name);
        Destroy(gameObject);
    }
    public override void Init(SpawnProjectile sp)
    {
        myColor = sp.currentInfoAmmo.myColor;
        this.GetComponent<Renderer>().material = materialsColor[(int)myColor];
        base.Init(sp);
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (firstContact)
            return;
        if (collision.transform.GetComponent<ElementDestructibleColor>() != null && collision.transform.GetComponent<ElementDestructibleColor>().myColor == myColor)
        {
            firstContact = true;
            collision.transform.GetComponent<ElementDestructibleColor>().ActiveMyEffectWithProjectil();
            DestroyMe();
        }
        else if (collision.transform.GetComponent<ElementDestructibleColor>() != null)
        {
            firstContact = true;

            Instantiate(FxFail, transform.position, Quaternion.identity);
            DestroyMe();
        }
        else if (collision.transform.GetComponent<ElementDestructibleBombe>())
        {
            firstContact = true;

            collision.transform.GetComponent<ElementDestructibleBombe>().ActiveMyEffectWithProjectil();
            DestroyMe();

        }
        else if (collision.transform.GetComponent<ElementDestructibleBonus>())
        {
            firstContact = true;

            collision.transform.GetComponent<ElementDestructibleBonus>().ActiveMyEffectWithProjectil();
            DestroyMe();

        }
        else
            base.OnCollisionEnter(collision);
        //    base.OnCollisionEnter(collision);
    }
    public ElementDestructible.Color myColor;
    public Material[] materialsColor;
    public GameObject FxFail;

}
