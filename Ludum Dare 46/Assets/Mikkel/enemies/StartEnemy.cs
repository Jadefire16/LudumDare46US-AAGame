using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class StartEnemy : Enemy
{

    private void Awake() {
        SetRb(GetComponent<Rigidbody>());
    }

    private void Start() {
        base.SetDetection();
        base.SetDetectionVal(0.5f);
        base.SetRange(10);
        base.SetSpeed(1);
        base.SetRotSpeed(2.5f);
        base.SetDamage(1);
        base.SetRun(3);
        base.SetAngle(90);
        base.SetRays(10);

    }

    private void Update() {
        base.Move();
    }



}
