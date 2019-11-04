using System;
using EZCameraShake;
using UnityEngine;

using Random = UnityEngine.Random;

public class ElementDestructible : MonoBehaviour
{

    public virtual void InitElement(GameManager gameManager, TypeOfElement mT)
    {
        myType = mT;
        gm = gameManager;
    }
    public virtual void ActiveMyEffectWithProjectil()
    {
        gm.AddScore(10);
    }
    public virtual void ActiveMyEffectWithBombe(float delay)
    {

    }


    protected virtual void ShakeEffect()
    {

    }
    protected virtual void SpawnFx()
    {

    }

    public virtual void DestroyMeWithDelay(float delay = 0)
    {
        if (delay > 0)
        {
            Invoke("DestroyMeNow", delay);
        }
        else
            DestroyMeNow();

        toDestroy = true;
    }

    public TypeOfElement GetTypeOfElement()
    {
        return myType;
    }
    public bool GetToDestroy()
    {
        return toDestroy;
    }

    public virtual void DestroyMeNow()
    {
        SpawnFx();
        ShakeEffect();
        Destroy(gameObject);
    }
    public enum TypeOfElement
    {
        Normal,
        Color,
        Explosive,
        Bonus
    }
    public enum Color
    {
        None = -1,
        Blue = 0,
        Red = 1,
        Yellow = 2
    }

    public TypeOfElement myType;
    public GameManager gm;

    protected bool toDestroy;
    protected float delayToDestroy;

}
