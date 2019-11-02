using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDestructibleBombe : ElementDestructible
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void InitElement(GameManager gameManager, TypeOfElement mT)
    {
        base.InitElement(gameManager, mT);
        gameObject.name += "Explosive";
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
      //  SpawnFx();
      //  ShakeEffect();
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
        if (gm.vibrationActivate)
            Handheld.Vibrate();
    }
    private void DestoyNeighbour(float delay = 0)
    {
        Collider[] elementInZone = Physics.OverlapSphere(this.transform.position, 1f);
        toDestroy = true;
        delayToDestroy = delay;
        foreach (var c in elementInZone)
        {
            if (c.gameObject != gameObject && c.GetComponent<ElementDestructible>() != null && !c.GetComponent<ElementDestructible>().GetToDestroy())
            {
               // if (c.GetComponent<ElementDestructible>().GetTypeOfElement() != TypeOfElement.Color)
                {
                    c.GetComponent<ElementDestructible>().ActiveMyEffectWithBombe(delayToDestroy + 0.2f);
                    Debug.Log(c.gameObject.name);
                }
                c.GetComponent<Rigidbody>().AddExplosionForce(1500, this.transform.position, 1000);

            }
            //       c.GetComponent<ElementDestructible>().DestroyMe();
        }
        GetComponent<Rigidbody>().AddExplosionForce(10000, transform.position, 80);
        DestroyMeWithDelay(delayToDestroy);
    }
    public Material materialBombe;
    public GameObject FXBombe;

}
