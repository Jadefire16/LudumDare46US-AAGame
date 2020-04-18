using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : Entity
{
    Vector3 towards, sides;
    float x, z;
    public LayerMask groundLayer;


    protected override void Start()
    {
        base.Start();
        speed = 5;
        towards = Camera.main.transform.forward;
        towards.y = 0;
        towards = Vector3.Normalize(towards);
        sides = Quaternion.Euler(new Vector3(0, 90, 0)) * towards;
    }

    private void Update() // quick movement 
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate() // call movement every fixed update
    {
        Move();
        GetGround();
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
    protected void Move() // Moves player using rb on z and z axis (not y)
    {
        Vector3 dir = new Vector3(x, 0, z);
        Vector3 rightMove = sides * speed * Time.fixedDeltaTime * dir.x;
        Vector3 upMove = towards * speed * Time.fixedDeltaTime * dir.z;

        Vector3 heading = Vector3.Normalize(rightMove + upMove);
        transform.forward = heading;
        transform.position += rightMove;
        transform.position += upMove;
    }

    void GetGround() {
        
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10f, groundLayer, QueryTriggerInteraction.Ignore)) {
            Quaternion.Lerp(transform.rotation, hit.transform.rotation, 1);
        }
       
    }

    private void Interact(Burnable burnable)
    {
        burnable.UseObject(this);
    }

    public int GetHealth()
    {
        int x = Health;
        return x;
    }
}