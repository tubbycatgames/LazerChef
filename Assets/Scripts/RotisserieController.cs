using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotisserieController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float shiftSpeed = 5f;
    public float shiftBound = 8f;
    public float pullSpeed = 15f;
    public float pullDiff = -3f;

    private float shiftMin;
    private float shiftMax;
    private float depthMin;
    private float depthMax;
    private int pullDirection;
    private bool isPullAdjusting = false;
    private bool isPulled = false;

    void Start()
    {
        var startShift = transform.position.x;
        shiftMin = transform.position.x - shiftBound;
        shiftMax = transform.position.x + shiftBound;

        var startDepth = transform.position.z;
        var pullDepth = startDepth + pullDiff;
        var pullInwards = pullDiff < 0;
        depthMin = pullInwards ? pullDepth : startDepth;
        depthMax = pullInwards ? startDepth : pullDepth;
        pullDirection = pullInwards ? -1 : 1;
    }

    void Update()
    {
        var verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            UpdateRotation(-verticalInput);
        }

        var horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            UpdateShift(horizontalInput);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPullAdjusting = true;
        }

        if (isPullAdjusting)
        {
            UpdatePull();
        }
    }

    void UpdateRotation(float direction)
    {
        var yDiff = rotationSpeed * direction * Time.deltaTime;
        transform.Rotate(0.0f, yDiff, 0.0f);
    }

    void UpdateShift(float direction)
    {
        var xDiff = direction * shiftSpeed * Time.deltaTime;
        var unclampedX = transform.position.x + xDiff;
        var newX = Mathf.Clamp(unclampedX, shiftMin, shiftMax);
        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }

    void UpdatePull()
    {
        var pullFactor = isPulled ? -pullDirection : pullDirection;
        var zDiff = pullSpeed * Time.deltaTime * pullFactor;
        var unclampedZ = transform.position.z + zDiff;
        var newZ = Mathf.Clamp(unclampedZ, depthMin, depthMax);

        if (newZ == depthMin || newZ == depthMax) 
        {
            isPullAdjusting = false;
            isPulled = !isPulled;
        }

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            newZ
        );
    }
}
