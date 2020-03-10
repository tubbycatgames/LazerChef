using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTestTarget : MonoBehaviour
{
    public Lazer lazer;
    public float height = 3f;
    public float jump = 2f;

    void Start()
    {
        transform.position = new Vector3(
            lazer.transform.position.x,
            lazer.transform.position.y + height,
            lazer.transform.position.z
        );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            var lazerX = lazer.transform.position.x;
            var newX = transform.position.x == lazerX
                ? lazerX + jump
                : lazerX;
            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
    }
}
