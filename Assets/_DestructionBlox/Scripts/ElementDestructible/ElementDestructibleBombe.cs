using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDestructibleBombe : ElementDestructible
{
    public override void InitElement(GameManager gameManager, TypeOfElement mT)
    {
        base.InitElement(gameManager, mT);
        gameObject.GetComponent<MeshRenderer>().material = materialBombe;
    }

    public override void ActiveMyEffectWithProjectil()
    {
        DestoyNeighbour();

        base.ActiveMyEffectWithProjectil();
    }

    public override void ActiveMyEffectWithBombe(float delay)
    {
        base.ActiveMyEffectWithBombe(delay);
        DestoyNeighbour(delay);
    }

    public override void DestroyMeNow()
    {
        base.DestroyMeNow();
    }
    public override void DestroyMeWithDelay(float delay = 0)
    {
        base.DestroyMeWithDelay(delay);
    }
    protected override void SpawnFx()
    {
        GameObject go = Instantiate(FXBombe, transform.position, Quaternion.identity);

        base.SpawnFx();
    }
    protected override void ShakeEffect()
    {
        base.ShakeEffect();
        CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(9.5f, 5.43f, 0.1f, 1);
        c.PositionInfluence = Vector3.one * 1;
        c.RotationInfluence = Vector3.one * 1;
        if (gm.GetVibrationActivate())
            Handheld.Vibrate();
    }
    private void DestoyNeighbour(float delay = 0)
    {
        Collider[] elementInZone = Physics.OverlapSphere(this.transform.position, 1f);
        toDestroy = true;
        delayToDestroy = delay;
        foreach (var c in elementInZone)
        {
            if (c.gameObject != gameObject)
            {
                if (c.TryGetComponent(out ElementDestructible eDestructible) && !eDestructible.GetToDestroy())
                {
                    eDestructible.ActiveMyEffectWithBombe(delayToDestroy + 0.2f);
                    c.GetComponent<Rigidbody>().AddExplosionForce(4000, this.transform.position, 3);
                }
            }
        }
        GetComponent<Rigidbody>().AddExplosionForce(4000, transform.position, 3);
        DestroyMeWithDelay(delayToDestroy);
    }
    public Material materialBombe;
    public GameObject FXBombe;

}
