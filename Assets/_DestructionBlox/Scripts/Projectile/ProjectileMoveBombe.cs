using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveBombe : ProjectileMove
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (firstContact)
            return;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb.GetComponent<ElementDestructible>() != null)
                rb.AddExplosionForce(power, explosionPos, radius);
        }
        GameObject go = Instantiate(FxBombe, transform.position, Quaternion.identity);
        if(sp.gameManager.vibrationActivate)
            Handheld.Vibrate();
        if (collision.transform.GetComponent<ElementDestructible>() != null)
        {
            firstContact = true;
            collision.transform.GetComponent<ElementDestructible>().ActiveMyEffectWithProjectil();
        }
        else
            base.OnCollisionEnter(collision);
       // Destroy(collision.gameObject);
        DestroyMe();
    }
    protected override void DestroyMe()
    {
        sp.CreateProjectile();
        Debug.Log(myTypeOfBall.ToString() + this.name);
        Destroy(gameObject);
    }
    public float radius = 5.0F;
    public float power = 10.0F;
    public GameObject FxBombe;

}
