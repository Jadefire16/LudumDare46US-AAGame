using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        rb.velocity = Vector3.right * speed;
    }

}
