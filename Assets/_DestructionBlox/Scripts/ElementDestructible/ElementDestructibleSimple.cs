using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDestructibleSimple : ElementDestructible
{

    public override void InitElement(GameManager gameManager, TypeOfElement mT)
    {
        gameObject.name += "Normal";
        gameObject.GetComponent<MeshRenderer>().material = materialNormal;

        base.InitElement(gameManager, mT);
    }
    public override void ActiveMyEffectWithProjectil()
    {
    }
    public Material materialNormal;

}
