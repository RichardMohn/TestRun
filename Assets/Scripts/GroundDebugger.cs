using UnityEngine;

public class GroundDebugger : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        Debug.Log("Ground Initial Position: " + initialPosition);
    }

    void Update()
    {
        if (transform.position != initialPosition)
        {
            Debug.Log("Ground Position Changed: " + transform.position);
            initialPosition = transform.position;
        }
    }
}