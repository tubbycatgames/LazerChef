using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotisserieController : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float shiftSpeed = 0.5f;
    public float maxShift = 5f;
    public float pullSpeed = 1f;
    public float pullDepth = 3f;

    private bool isPullAdjusting = false;
    private bool isPulled = false;

    void Update()
    {
        UpdateRotation();
        UpdateShift();
        UpdatePull();
    }

    void UpdateRotation()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var newRotation = rotationSpeed * verticalInput * Time.deltaTime;
        transform.Rotate(0.0f, newRotation, 0.0f);
    }

    void UpdateShift()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var newX = (horizontalInput * shiftSpeed * Time.deltaTime) + transform.position.x;
        if (Mathf.Abs(newX) < maxShift)
        {
            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
    }

    void UpdatePull()
    {
        if (isPullAdjusting)
        {
            var newZ = transform.position.z;
            var pullDiff = pullSpeed * Time.deltaTime;

            if (isPulled) 
            {
                newZ = Mathf.Max(newZ - pullDiff, 0f);
            }
            else 
            {
                newZ = Mathf.Min(newZ + pullDiff, maxShift);
            }

            if (newZ == 0f || newZ == maxShift) {
                isPullAdjusting = false;
            }

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                newZ
            );

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPullAdjusting = true;
                isPulled = !isPulled;
            }
        }
    }
}
