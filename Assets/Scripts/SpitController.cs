using UnityEngine;

public class SpitController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float swapSpeed = 10f;
    public GameObject food;

    private bool isFoodSwapping = false;
    private int swapDirection = 1;
    private float swapPoint;
    private float swapBuffer = 3f;

    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.Return) && !isFoodSwapping)
        {
            isFoodSwapping = true;
            // SFX Food being Removed from Spit
        }

        if (isFoodSwapping)
        {
            SwapFood();
        }
    }

    void UpdateRotation(float direction)
    {
        var xDiff = rotationSpeed * direction * Time.deltaTime;
        transform.Rotate(xDiff, 0.0f, 0.0f);

        // SFX Spit Being Rotated
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
                // SFX Food Being Added to Spit
            }
        }
    }
}
