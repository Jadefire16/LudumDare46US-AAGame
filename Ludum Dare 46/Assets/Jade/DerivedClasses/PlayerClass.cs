using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : Entity
{
    Vector3 input;

    protected override void Start()
    {
        base.Start();
        rb.isKinematic = false;
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move(input);
    }

    protected void ResetPlayer()
    {
        transform.position = GameManager.instance.CurrentCheckpoint;
        Health = GameManager.instance.PlayerMaxLives;
    }

    protected override void KillEntity()
    {
        base.KillEntity(); // this will destroy player, remove if you wanna just want to reset it
    }

    protected override void Attack()
    {
        
    }
    protected override void Move(Vector3 dir)
    {
        rb.MovePosition(rb.position + (dir * speed * Time.deltaTime));
    }
}
