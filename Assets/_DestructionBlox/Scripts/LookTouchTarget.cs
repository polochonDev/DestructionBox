using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTouchTarget : MonoBehaviour
{
    void Update()
    {
        if (cam != null)
        {
            RaycastHit hit;
            Vector3 mousePos = Vector3.zero;
#if UNITY_EDITOR
            mousePos = Input.mousePosition;

#elif UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount == 0)
                return;
            mousePos = Input.GetTouch(0).position;

#endif
            rayMouse = cam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximumLenght))
                RotateToMouseDirection(gameObject, hit.point);
            else
            {
                var pos = rayMouse.GetPoint(maximumLenght);
                RotateToMouseDirection(gameObject, pos);
            }
        }
        else
            Debug.Log("No camera");
    }
    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.rotation = rotation;
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float maximumLenght;
    [SerializeField]
    private Transform graphicCanon;
    private Ray rayMouse;
    private Vector3 direction;
    private Quaternion rotation;

}
