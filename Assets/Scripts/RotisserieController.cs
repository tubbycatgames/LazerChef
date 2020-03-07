using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotisserieController : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float shiftSpeed = 0.5f;
    public float maxShift = 5f;

    void Update()
    {
        UpdateRotation();
        UpdateShift();
    }

    void UpdateRotation()
    {
        var verticalInput = Input.GetAxis("Vertical");
        transform.Rotate(0.0f, rotationSpeed * verticalInput, 0.0f);
    }

    void UpdateShift()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var newX = (horizontalInput * shiftSpeed) + transform.position.x;
        if (Mathf.Abs(newX) < maxShift)
        {
            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
    }
}
