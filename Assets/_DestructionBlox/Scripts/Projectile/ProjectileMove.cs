using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public virtual void Init(SpawnProjectile spawnProjectile)
    {
        sp = spawnProjectile;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (firstContact)
            return;
        if (collision.transform.name.StartsWith("limit"))
        {
            if (!firstContact)
                DestroyMe();
        }
    }
    protected virtual void DestroyMe()
    {
        sp.CreateProjectile();
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
