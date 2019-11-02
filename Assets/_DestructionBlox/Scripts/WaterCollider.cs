using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WaterBob>() != null)
        {
            other.GetComponent<WaterBob>().enabled = true;
            if (other.GetComponent<ElementDestructible>())
                other.GetComponent<ElementDestructible>().ActiveMyEffectWithProjectil();
        }
    }
}
