using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{


    public Transform target;

    [SerializeField] float xRot = 45;
    [SerializeField] float yRot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            transform.position = target.position;
            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
            transform.position = transform.position + transform.forward * -50f;
        }
    }
}
