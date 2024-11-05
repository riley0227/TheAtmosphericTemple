using UnityEngine;

public class AxeRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of the spin

    void Update()
    {
        // Rotate around the z-axis of axe
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
