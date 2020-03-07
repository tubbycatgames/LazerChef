using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotisserieController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float shiftSpeed = 5f;
    public float maxShift = 8f;
    public float pullSpeed = 15f;
    public float pullDepth = -43f;

    private float startDepth;
    private bool isPullAdjusting = false;
    private bool isPulled = false;

    void Start()
    {
        startDepth = transform.position.z;
    }

    void Update()
    {
        UpdateRotation();
        UpdateShift();
        UpdatePull();
    }

    void UpdateRotation()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var yDiff = rotationSpeed * verticalInput * Time.deltaTime * -1;
        transform.Rotate(0.0f, yDiff, 0.0f);
    }

    void UpdateShift()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var xDiff = Input.GetAxis("Horizontal") * shiftSpeed * Time.deltaTime;
        var newX = transform.position.x + xDiff;
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
                newZ = Mathf.Min(newZ + pullDiff, startDepth);
            }
            else 
            {
                newZ = Mathf.Max(newZ - pullDiff, pullDepth);
            }

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                newZ
            );

            
            if (newZ == 0f || newZ == pullDepth) 
            {
                isPullAdjusting = false;
                isPulled = !isPulled;
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPullAdjusting = true;
            }
        }
    }
}
