using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public virtual void Init(SpawnProjectile spawnProjectile)
    {
        sp = spawnProjectile;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (speed != 0)
       //     transform.position += transform.forward * (speed * Time.deltaTime);
       // else
       //     Debug.Log("No speed");
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (firstContact)
            return;
/*        if (collision.transform.GetComponent<ElementDestructible>() != null)
        {
            firstContact = true;
            collision.transform.GetComponent<ElementDestructible>().ActiveMyEffectWithProjectil();
            DestroyMe();
        }*/
        if (collision.transform.name.StartsWith("limit"))
        {
            if (!firstContact)
                DestroyMe();
        }
    }
    protected virtual void DestroyMe()
    {
        sp.CreateProjectile();
        Debug.Log(myTypeOfBall.ToString() + this.name);
        Destroy(gameObject);
    }
    protected bool firstContact = false;
    protected SpawnProjectile sp;
    public TypeOfBall myTypeOfBall;
    public enum TypeOfBall
    {
        Normal,
        Color,
        Explosive,
        Bouncing
    }
}
