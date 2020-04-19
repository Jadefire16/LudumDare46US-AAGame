using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody body;
    Light lightSource;
    Vector3 inputVec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        lightSource = GetComponentInChildren<Light>();
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");


        inputVec = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * inputVec;



        if (Input.GetButton("Jump"))
        {

            body.velocity = Vector3.up * 5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        Vector3 velocity = -body.velocity;
        velocity.y = 0;

        body.AddForce(velocity * 0.2f, ForceMode.VelocityChange);

        body.AddForce(inputVec * 0.7f, ForceMode.VelocityChange);
    }
}
