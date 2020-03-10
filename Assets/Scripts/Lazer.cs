using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public float maxDistance = 5000f;
    private LineRenderer beam;

    void Start()
    {
        beam = GetComponent<LineRenderer>();
        beam.SetPosition(0, transform.position);
    }

    void Update()
    {
        var up = transform.up;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, up, out hit) && hit.collider)
        {
            beam.SetPosition(1, hit.point);
        }
        else
        {
            beam.SetPosition(1, up * maxDistance);
        }
    }
}
