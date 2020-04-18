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

    private void Update() // quick movement 
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() // call movement every fixed update
    {
        Move(input);
    }

    protected void ResetPlayer() // used to reset players values when they die after level reload
    {
        transform.position = GameManager.instance.CurrentCheckpoint;
        Health = GameManager.instance.PlayerMaxLives;
    }

    protected override void KillEntity()
    {
        base.KillEntity(); // this will destroy Entity class, remove if you wanna just want to reset it
        EventManager.instance.CallPlayerDeath();
    }

    protected override void Attack() // pretty straight forward, make the player lose a life and attack
    {
        
    }
    protected override void Move(Vector3 dir) // Moves player using rb on z and z axis (not y)
    {
        rb.MovePosition(rb.position + (dir * speed * Time.deltaTime));
    }

    private void Interact(Burnable burnable)
    {
        burnable.UseObject(this);
    }

}
