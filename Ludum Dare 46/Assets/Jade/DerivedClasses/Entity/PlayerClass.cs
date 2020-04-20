using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : Entity
{
    Vector3 towards, sides;
    float x, z;
    private float jumpForce = 6f;

    [Space]
    [Header("Health")]

    public Image[] fire;
    public Sprite fireLit, fireUnlit;
    public int fireVal;
    bool canMove = true, canJump = true;


    protected override void Start()
    {
        base.Start();
        speed = 3.5f;
        towards = Camera.main.transform.forward;
        towards.y = 0;
        towards = Vector3.Normalize(towards);
        sides = Quaternion.Euler(new Vector3(0, 90, 0)) * towards;
        canJump = true;
        SaveEntityData();

    }

    private void Update() // quick movement 
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        for (int i = 0; i < fire.Length; i++)
        {
            if (i < Health)
            {
                fire[i].sprite = fireLit;
            }
            else
            {
                fire[i].sprite = fireUnlit;
            }

            if (i < fireVal)
            {
                fire[i].enabled = true;
            }
            else
            {
                fire[i].enabled = false;
            }

        }
        if (Health <= 0)
        {
            fire[0].sprite = fireUnlit;
        }

    }
    private void FixedUpdate() // call movement every fixed update
    {
        if (canMove)
        {
            Move();
            //GetGround();
        }
    }
    protected override void SaveEntityData()
    {
        base.SaveEntityData();
    }

    protected override void LoadEntityData()
    {
        base.LoadEntityData();
    }
    protected override void DeleteEntityData()
    {
        base.DeleteEntityData();
    }

    protected override void SyncDataToEntity()
    {
        base.SyncDataToEntity();
    }
    protected override void SyncEntityToData()
    {
        base.SyncEntityToData();
    }
    protected override void KillEntity()
    {
        canMove = false;
        canJump = false;
        LoadEntityData();
    }
    protected override void Attack() // pretty straight forward, make the player lose a life and attack
    {
        Health--;
        //add shooting
        StartCoroutine(Wait(2));
    }

    void Jump()
    {
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        StartCoroutine(Wait(1));
    }

    protected void ResetPlayer() // kinda useless keeping it just in case
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

    void GetGround()
    {

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10f, groundLayer, QueryTriggerInteraction.Ignore))
        {
            transform.rotation = hit.transform.rotation;
            //Quaternion desiredRot = Quaternion.LookRotation(hit.normal);
            //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * 5f);
        
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

    IEnumerator Wait(float sec)
    {
        canJump = false;
        yield return new WaitForSeconds(sec);
        canJump = true;
    }

}