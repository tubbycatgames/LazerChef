using UnityEngine;

public class RotisserieController : MonoBehaviour
{
    public float pullSpeed = 15f;
    public float pullDiff = -3f;

    private float heightMin;
    private float heightMax;

    private int pullDirection;
    private bool isPullAdjusting = false;
    private bool isPulled = false;

    void Start()
    {
        var startHeight = transform.position.y;
        var pullHeight = startHeight + pullDiff;
        var pullDownwards = pullDiff < 0;
        heightMin = pullDownwards ? pullHeight : startHeight;
        heightMax = pullDownwards ? startHeight : pullHeight;
        pullDirection = pullDownwards ? -1 : 1;
    }

    void Update()
    {

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
    }

    void UpdatePull()
    {
        var pullFactor = isPulled ? -pullDirection : pullDirection;
        var yDiff = pullSpeed * Time.deltaTime * pullFactor;
        var unclampedY = transform.position.y + yDiff;
        var newY = Mathf.Clamp(unclampedY, heightMin, heightMax);

        if (newY == heightMin || newY == heightMax)
        {
            isPullAdjusting = false;
            isPulled = !isPulled;
        }

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }

}
