using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public IEnumerator Make180()
    {
        float startAngle = transform.eulerAngles.y;
        float targetAngle = 0;
        if (startAngle > 180)
            targetAngle = 0;
        else
            targetAngle = 180;
        Debug.Log("Make180" + transform.eulerAngles.y);
        while (transform.eulerAngles.y < targetAngle)
        {
            Debug.Log("in" + transform.eulerAngles.y);

            Vector2 TouchDirection = new Vector2(8, 0);
            Vector3 worldVector = Camera.main.ScreenToWorldPoint(new Vector3(TouchDirection.x, TouchDirection.y, 0));

            Vector2 SpinDirection = new Vector2(TouchDirection.x - (Camera.main.pixelWidth * 0.5f), TouchDirection.y - (Camera.main.pixelHeight * 0.5f)) * 2f;

            transform.RotateAround(Target.transform.position, Vector3.up, TouchDirection.x * SpeedMultiplier);
            yield return null;
        }

        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 TouchDirection = new Vector2(1, 0);
            Vector3 worldVector = Camera.main.ScreenToWorldPoint(new Vector3(TouchDirection.x, TouchDirection.y, 0));

            Vector2 SpinDirection = new Vector2(TouchDirection.x - (Camera.main.pixelWidth * 0.5f), TouchDirection.y - (Camera.main.pixelHeight * 0.5f)) * 2f;

            transform.RotateAround(Target.transform.position, Vector3.up, TouchDirection.x * SpeedMultiplier);
            //transform.RotateAround(player.transform.position, Vector3.right, TouchDirection.y * 0.2f);

            //Y_Yaw.transform.eulerAngles = new Vector3(SpinDirection.y * 0.1f, 0f, 0f);

         //CameraTransform.LookAt(Target.transform.position);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 TouchDirection = new Vector2(-1, 0);
           // Vector3 worldVector = Camera.main.ScreenToWorldPoint(new Vector3(TouchDirection.x, TouchDirection.y, 0));

            Vector2 SpinDirection = new Vector2(TouchDirection.x - (Camera.main.pixelWidth * 0.5f), TouchDirection.y - (Camera.main.pixelHeight * 0.5f)) * 2f;

            transform.RotateAround(Target.transform.position, Vector3.up, TouchDirection.x * SpeedMultiplier);
            //transform.RotateAround(player.transform.position, Vector3.right, TouchDirection.y * 0.2f);

            //Y_Yaw.transform.eulerAngles = new Vector3(SpinDirection.y * 0.1f, 0f, 0f);

         //   CameraTransform.LookAt(Target.transform.position);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 TouchDirection = Input.GetTouch(0).deltaPosition;
        //    Vector3 worldVector = Camera.main.ScreenToWorldPoint(new Vector3(TouchDirection.x, TouchDirection.y, 0));

            Vector2 SpinDirection = new Vector2(TouchDirection.x - (Camera.main.pixelWidth * 0.5f), TouchDirection.y - (Camera.main.pixelHeight * 0.5f)) * 2f;

            transform.RotateAround(Target.transform.position, Vector3.up, TouchDirection.x * SpeedMultiplier);
            //transform.RotateAround(player.transform.position, Vector3.right, TouchDirection.y * 0.2f);

            //Y_Yaw.transform.eulerAngles = new Vector3(SpinDirection.y * 0.1f, 0f, 0f);

        //    CameraTransform.LookAt(Target.transform.position);
        }
    }
    public float SpeedMultiplier = 0.37f;
    public GameObject Target;
    public Transform CameraTransform;
}
