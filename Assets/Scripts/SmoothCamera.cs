using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    void LateUpdate()
    {
        var newPos = Vector3.Lerp(transform.position, target.position, speed);
        newPos.z = -10;
        transform.position = newPos;

    }
}
