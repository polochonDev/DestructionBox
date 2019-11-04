using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public IEnumerator Make180()
    {
        float startAngle = transform.eulerAngles.y;
        float targetAngle = 0;
        targetAngle = 180;
        while (transform.eulerAngles.y < targetAngle)
        {
            Vector2 touchDirection = new Vector2(8, 0);

            transform.RotateAround(target.transform.position, Vector3.up, touchDirection.x * speedMultiplier);
            yield return null;
        }
        yield return null;
    }

    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 touchDirection = new Vector2(1, 0);

            transform.RotateAround(target.transform.position, Vector3.up, touchDirection.x * speedMultiplier);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 touchDirection = new Vector2(-1, 0);

            transform.RotateAround(target.transform.position, Vector3.up, touchDirection.x * speedMultiplier);
        }
#elif UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDirection = Input.GetTouch(0).deltaPosition;

            transform.RotateAround(target.transform.position, Vector3.up, touchDirection.x * speedMultiplier);
        }
#endif
    }
    public float speedMultiplier = 0.37f;
    public GameObject target;
}
