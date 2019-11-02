using System;
using EZCameraShake;
using UnityEngine;

using Random = UnityEngine.Random;

public class ElementDestructible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void InitElement(GameManager gameManager, TypeOfElement mT)
    {
        myType = mT;
        gm = gameManager;
     //   this.GetComponent<MeshRenderer>().material = myMaterial;
        switch (mT)
        {
            case TypeOfElement.Normal:
  //              gameObject.name += "Normal";
  //              gameObject.GetComponent<MeshRenderer>().material = materialNormal;
                break;
            case TypeOfElement.Color:
                /*gameObject.name += "Color";

                myColor = (Color)Random.Range(0,3);
                gameObject.GetComponent<MeshRenderer>().material = materialsColor[(int)myColor];
                */
                break;
            case TypeOfElement.Explosive:
             //   gameObject.name += "Explosive";
             //   gameObject.GetComponent<MeshRenderer>().material = materialBombe;

                break;
            case TypeOfElement.Bonus:
            //    gameObject.name += "Bonus";
            //    gameObject.GetComponent<MeshRenderer>().material = materialBonus;

                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void ActiveMyEffectWithProjectil()
    {
        gm.AddScore(10);
    }
    public virtual void ActiveMyEffectWithBombe(float delay)
    {
        Debug.Log(myType.ToString());
        switch (myType)
        {
            case TypeOfElement.Normal:
                break;
            case TypeOfElement.Color:
            //    CheckNeighbourColor();
                break;
            case TypeOfElement.Explosive:
            //    DestoyNeighbour(delay);
                break;
            case TypeOfElement.Bonus:
             //   ApplyBonus();
             //   DestroyMeWithDelay(delay);
                break;
            default:
                break;
        }
     //   gm.AddScore(10);
     //   DestroyMeWithDelay(delay);
    }



  /*  protected void DestoyNeighbour(float delay = 0)
    {
        Collider[] elementInZone = Physics.OverlapSphere(this.transform.position, 0.75f);
        toDestroy = true;
        delayToDestroy = delay;
        foreach (var c in elementInZone)
        {
            if (c.gameObject != gameObject && c.GetComponent<ElementDestructible>() != null && !c.GetComponent<ElementDestructible>().toDestroy)
            {
                if(c.GetComponent<ElementDestructible>().myType != TypeOfElement.Color)
                {
                    c.GetComponent<ElementDestructible>().ActiveMyEffectWithBombe(delayToDestroy + 0.2f);

                    Debug.Log(c.gameObject.name);
                }


            }

            //       c.GetComponent<ElementDestructible>().DestroyMe();
        }
        GetComponent<Rigidbody>().AddExplosionForce(10000, transform.position, 80);

        DestroyMeWithDelay(delayToDestroy);
    }*/

 /*   public void CheckNeighbourColor()
    {
        Collider[] elementInZone = Physics.OverlapSphere(this.transform.position, 0.75f);
        toDestroy = true;

        foreach (var c in elementInZone)
        {

            if (c.gameObject != gameObject && c.GetComponent<ElementDestructible>() != null && !c.GetComponent<ElementDestructible>().toDestroy)
            {
                Debug.Log(c.gameObject.name);
                c.GetComponent<ElementDestructible>().CheckNeighbourColor(myColor, delayToDestroy + 0.1f);
            }

            //       
        }
        DestroyMeWithDelay();


    }*/

    protected virtual void ShakeEffect()
    {
    /*    if (myType == TypeOfElement.Color)
        {
            CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(.36f, 10f, 0.2f, 2f);
            c.PositionInfluence = Vector3.one* 0.2f;
            c.RotationInfluence = Vector3.one * 0.2f;
        }
        else if (myType == TypeOfElement.Explosive)
        {
            CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(9.5f, 5.43f, 0.1f, 1);
            c.PositionInfluence = Vector3.one *1;
            c.RotationInfluence = Vector3.one *1;
        }*/
    }
    protected virtual void SpawnFx()
    {
        if(myType == TypeOfElement.Color)
        {
        //    GameObject go = Instantiate(FXColor[(int)myColor], transform.position, Quaternion.identity);
        }
        else if (myType == TypeOfElement.Explosive)
        {
          //  GameObject go = Instantiate(FXBombe, transform.position, Quaternion.identity);

        }
        else if (myType == TypeOfElement.Bonus)
        {
       //     GameObject go = Instantiate(FxBonus, transform.position, Quaternion.identity);
        }
    }

 /*   public void CheckNeighbourColor(Color color, float timer)
    {
        if (myType == TypeOfElement.Color && color != Color.None && color == myColor)
        {
            Collider[] elementInZone = Physics.OverlapSphere(this.transform.position, 1f);
            toDestroy = true;

            foreach (var c in elementInZone)
            {
                if (c.gameObject != gameObject && c.GetComponent<ElementDestructible>() != null && !c.GetComponent<ElementDestructible>().toDestroy)
                {
                    Debug.Log(c.gameObject.name);
                    delayToDestroy = timer;
                    c.GetComponent<ElementDestructible>().CheckNeighbourColor(color, timer + 0.1f);
                    DestroyMeWithDelay(delayToDestroy);
                }

            }

        }
    }*/
    public virtual void DestroyMeWithDelay(float delay = 0)
    {
        if(delay > 0)
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
 //   public Color myColor = Color.None;
    public GameManager gm;
  //  public Material materialBombe;
 //   public Material[] materialsColor;
 //   public GameObject[] FXColor;
//    public GameObject FXBombe;
    protected bool toDestroy;
    protected float delayToDestroy;

}
