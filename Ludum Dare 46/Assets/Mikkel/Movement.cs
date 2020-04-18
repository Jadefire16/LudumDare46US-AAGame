using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Movement : MonoBehaviour
{

    public float speed;

    private Vector3 towards, sides;

    private void Start() {
        towards = Camera.main.transform.forward;
        towards.y = 0;
        towards = Vector3.Normalize(towards);
        sides = Quaternion.Euler(new Vector3(0, 90, 0)) * towards;
    }

    private void Update() {

        if (Input.anyKey) {
            Move();
        }

    }

    void Move() {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMove = sides * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMove = towards * speed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMove + upMove);
        transform.forward = heading;
        transform.position += rightMove;
        transform.position += upMove;

    }

}
