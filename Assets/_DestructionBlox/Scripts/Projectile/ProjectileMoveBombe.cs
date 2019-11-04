using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveBombe : ProjectileMove
{
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
        if (sp.gameManager.GetVibrationActivate())
            Handheld.Vibrate();
        if (collision.transform.TryGetComponent(out ElementDestructible element))
        {
            firstContact = true;
            element.ActiveMyEffectWithProjectil();
        }
        else
            base.OnCollisionEnter(collision);
        DestroyMe();
    }
    protected override void DestroyMe()
    {
        sp.CreateProjectile();
        Destroy(gameObject);
    }

    [SerializeField]
    private float radius = 5.0F;
    [SerializeField]
    private float power = 10.0F;
    [SerializeField]
    private GameObject FxBombe;

}
