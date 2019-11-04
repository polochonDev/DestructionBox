using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDestructibleColor : ElementDestructible
{

    public override void InitElement(GameManager gameManager, TypeOfElement mT)
    {
        myColor = (Color)Random.Range(0, 3);
        gameObject.GetComponent<MeshRenderer>().material = materialsColor[(int)myColor];

        base.InitElement(gameManager, mT);
    }
    public override void ActiveMyEffectWithProjectil()
    {
        CheckNeighbourColor();
        base.ActiveMyEffectWithProjectil();
    }
    public override void ActiveMyEffectWithBombe(float delay)
    {
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
        GameObject go = Instantiate(FXColor[(int)myColor], transform.position, Quaternion.identity);

        base.SpawnFx();
    }
    public void CheckNeighbourColor()
    {
        Collider[] elementInZone = Physics.OverlapSphere(this.transform.position, 1.5f);
        toDestroy = true;

        foreach (var c in elementInZone)
        {
            if (c.gameObject != gameObject)
            {
                if (c.TryGetComponent(out ElementDestructibleColor eColor) && !eColor.toDestroy)
                {
                    eColor.CheckNeighbourColor(myColor, delayToDestroy + 0.1f, 10);
                }
            }
        }
        DestroyMeWithDelay();
    }

    public void CheckNeighbourColor(Color color, float timer, int score)
    {
        if (myType == TypeOfElement.Color && color != Color.None && color == myColor)
        {
            Collider[] elementInZone = Physics.OverlapSphere(this.transform.position, 1.5f);
            toDestroy = true;

            foreach (var c in elementInZone)
            {
                if (c.gameObject != gameObject)
                {
                    if (c.TryGetComponent(out ElementDestructibleColor eColor) && !eColor.toDestroy)
                    {
                        delayToDestroy = timer;
                        eColor.CheckNeighbourColor(color, timer + 0.1f, score + 5);
                        DestroyMeWithDelay(delayToDestroy);
                    }
                }
            }
            gm.AddScore(score);

        }
    }
    public Color myColor = Color.None;
    [SerializeField]
    private Material[] materialsColor;
    [SerializeField]
    private GameObject[] FXColor;

}
