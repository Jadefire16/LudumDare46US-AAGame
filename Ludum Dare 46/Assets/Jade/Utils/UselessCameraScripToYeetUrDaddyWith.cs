using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessCameraScripToYeetUrDaddyWith : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    Transform t;

    private void Start()
    {
        t = transform;
        t.position = (target.position + offset);
    }

    private void LateUpdate()
    {
        t.position = (target.position + offset);
    }
}
