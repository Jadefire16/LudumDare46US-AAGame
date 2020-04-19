using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessCameraScripToYeetUrDaddyWith : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    Transform t;
    public float smoothing = 8f;

    private void Start()
    {
        t = transform;
        t.position = (target.position + offset);
    }

    private void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(currentPos, desiredPos, smoothing * Time.fixedDeltaTime);
        t.position = smoothPos;
    }
}
        //t.position = (target.position + offset);
