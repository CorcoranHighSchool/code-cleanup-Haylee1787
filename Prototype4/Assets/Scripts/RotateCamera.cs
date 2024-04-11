using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializedField] float rotationSpeed;
    private const string horizontal = "Horizontal";

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, (horizontalInput * rotationSpeed * Time.deltaTime));
    }
}
