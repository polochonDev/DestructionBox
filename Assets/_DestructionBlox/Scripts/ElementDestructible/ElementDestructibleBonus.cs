using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDestructibleBonus : ElementDestructible
{
    public override void InitElement(GameManager gameManager, TypeOfElement mT)
    {
        gameObject.GetComponent<MeshRenderer>().material = materialBonus;

        base.InitElement(gameManager, mT);
    }
    public override void ActiveMyEffectWithProjectil()
    {
        ApplyBonus();
        DestroyMeWithDelay();
        base.ActiveMyEffectWithProjectil();
    }
    public override void ActiveMyEffectWithBombe(float delay)
    {
        ApplyBonus();
        DestroyMeWithDelay();
        base.ActiveMyEffectWithBombe(delay);
    }
    public override void DestroyMeNow()
    {
        base.DestroyMeNow();
    }
    public override void DestroyMeWithDelay(float delay = 0)
    {
        base.DestroyMeWithDelay(delay);
    }
    protected override void ShakeEffect()
    {
        base.ShakeEffect();
    }
    protected override void SpawnFx()
    {
        GameObject go = Instantiate(FxBonus, transform.position, Quaternion.identity);

        base.SpawnFx();
    }

    private void ApplyBonus()
    {
        gm.AddScore(100);
    }
    public GameObject FxBonus;
    public Material materialBonus;

}
