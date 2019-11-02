using UnityEngine;

public class WaterBob : MonoBehaviour
{
    [SerializeField]
    float height = 0.1f;

    [SerializeField]
    float period = 1;

    private Vector3 initialPosition;
    private float offset;
    private void OnEnable()
    {
        initialPosition = transform.position;

        offset = 1 - (Random.value * 2);
        if(transform.GetComponent<Rigidbody>() != null)
            transform.GetComponent<Rigidbody>().freezeRotation = true;
    }


    private void Update()
    {
        transform.position = initialPosition - Vector3.up * Mathf.Sin((Time.time + offset) * period) * height;

    }
}
