using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDestructibleSimple : ElementDestructible
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
        gameObject.name += "Normal";
        gameObject.GetComponent<MeshRenderer>().material = materialNormal;

        base.InitElement(gameManager, mT);
    }
    public override void ActiveMyEffectWithProjectil()
    {
    //    base.ActiveMyEffectWithProjectil();
    }
    public Material materialNormal;

}
