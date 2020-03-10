using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitController : MonoBehaviour
{
    public float rotationSpeed = 50f;

    public float shiftSpeed = 5f;
    public float shiftBound = 8f;

    public float pullSpeed = 15f;
    public float pullDiff = -3f;

    public float swapSpeed = 10f;

    private float shiftMin;
    private float shiftMax;

    private float depthMin;
    private float depthMax;

    private int pullDirection;
    private bool isPullAdjusting = false;
    private bool isPulled = false;

    private GameObject food;
    private bool isFoodSwapping = false;
    private int swapDirection = 1;
    private float swapPoint;
    private float swapBuffer = 3f;

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

        if (transform.childCount == 1) 
        {
            food = transform.GetChild(0).gameObject;
        }
        var screenEdgeAtSpit = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width, 
            Screen.height, 
            transform.position.z - Camera.main.transform.position.z
        ));
        swapPoint = screenEdgeAtSpit.x + swapBuffer;
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

        if (Input.GetKeyDown(KeyCode.Space) && !isPullAdjusting)
        {
            isPullAdjusting = true;
            // SFX Spit Being Put into Lazer
            if (isPulled) 
            {

            }
            // SFX Spit Being Pulled Away From Lazer
            else
            {

            }
        }

        if (isPullAdjusting)
        {
            UpdatePull();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            isFoodSwapping = true;
        }

        if (isFoodSwapping)
        {
            SwapFood();
        }
    }

    void UpdateRotation(float direction)
    {
        var yDiff = rotationSpeed * direction * Time.deltaTime;
        transform.Rotate(0.0f, yDiff, 0.0f);

        // SFX Spit Being Rotated
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

        // SFX Spit Being Shifted
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

    void SwapFood()
    {
        var xDiff = swapSpeed * Time.deltaTime * swapDirection;
        var unclampedX = food.transform.position.x + xDiff;
        var spitX = transform.position.x;
        var newX = Mathf.Clamp(unclampedX, spitX, swapPoint);

        food.transform.position = new Vector3(
            newX,
            food.transform.position.y,
            food.transform.position.z
        );
        
        if (newX == spitX || newX == swapPoint) 
        {
            swapDirection *= -1;

            if (newX == spitX)
            {
                isFoodSwapping = false;
            }
            else 
            {
                var newFood = Instantiate(food, transform);
                Destroy(food);
                food = newFood;
            }
        }
    }
}
