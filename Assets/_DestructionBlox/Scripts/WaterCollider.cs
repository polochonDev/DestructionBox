using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WaterBob water))
        {
            water.enabled = true;
            if (other.TryGetComponent(out ElementDestructible element))
                element.ActiveMyEffectWithProjectil();
        }
    }
}
