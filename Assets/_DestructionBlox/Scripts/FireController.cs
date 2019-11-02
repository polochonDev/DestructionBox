using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * 5 / 100; //dragDistance is 15% height of the screen
    }

    public GameObject projectile;
    public float bulletSpeed;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;  
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    //drag
                }
                else
                {
                    //tap
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector2 dir = touchPos - (new Vector2(transform.position.x, transform.position.y));
                    dir.Normalize();
                    GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;

                }
            }

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody != null)
                {

                }
            }

        }
    }
}
