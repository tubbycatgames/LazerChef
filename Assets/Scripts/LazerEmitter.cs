using UnityEngine;

public class LazerEmitter : MonoBehaviour
{
    public float shiftSpeed = 5f;
    public float shiftBound = 8f;
    public float maxDistance = 5000f;

    private LineRenderer beam;
    private float shiftMin;
    private float shiftMax;


    void Start()
    {
        beam = GetComponent<LineRenderer>();

        var startShift = transform.position.x;
        shiftMin = transform.position.x - shiftBound;
        shiftMax = transform.position.x + shiftBound;
    }

    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            UpdateShift(horizontalInput);
        }

        EmitBeam();
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

        // SFX Lazer Being Shifted
    }

    private void EmitBeam()
    {
        beam.SetPosition(0, transform.position);

        var up = transform.up;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, up, out hit) && hit.collider)
        {
            beam.SetPosition(1, hit.point);
            // SFX Lazer Striking Object
        }
        else
        {
            beam.SetPosition(1, up * maxDistance);
            // SFX Lazer emitting without strike
        }
    }
}
