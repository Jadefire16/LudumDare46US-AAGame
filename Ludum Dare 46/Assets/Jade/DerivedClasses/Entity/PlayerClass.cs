using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : Entity
{
    Vector3 towards, sides;
    float x, z;
    public float jumpForce = 2.5f, rayLength;
    public LayerMask groundLayer;

    public GameObject fireball, origin, player;

    public Fireball fireBall;

    bool canJump = true;


    protected override void Start()
    {
        base.Start();
        speed = 5;
        towards = Camera.main.transform.forward;
        towards.y = 0;
        towards = Vector3.Normalize(towards);
        sides = Quaternion.Euler(new Vector3(0, 90, 0)) * towards;
        canJump = true;
    }

    private void Update() // quick movement 
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Attack();
        }

    }

    private void FixedUpdate() // call movement every fixed update
    {
        Move();
        GetGround();
    }

    void Jump() {
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        StartCoroutine(Wait(1));
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
        Health--;
        //add shooting
        StartCoroutine(Wait(2));
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
            // transform.rotation = hit.transform.rotation;
            Quaternion desiredRot = Quaternion.LookRotation(hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * 5f);
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

    IEnumerator Wait(float sec) {
        canJump = false;
        yield return new WaitForSeconds(sec);
        canJump = true;
    }

}