﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClass : Entity
{
    private float jumpForce = 6f;
    public Image[] fire;

    public Sprite fireLit, fireUnlit;
    public int fireVal;
    bool canMove = true;
    bool canJump = true;
   [SerializeField] Vector3 inputVec = Vector3.zero;

    protected override void Start()
    {
        base.Start();
        canJump = true;
    }

    private void Update() // quick movement 
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        if (inputVec.magnitude >= 1f)
        {
            inputVec.Normalize();
        }
        inputVec = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * inputVec;

        if (Input.GetButton("Jump"))
        {
            if (canJump)
                Jump();
        }


        if (Input.GetKeyDown(KeyCode.J)) {
            TakeDamage(-1);
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            TakeDamage(1);
        }
        fireVal = maxHealth;
        for (int i = 0; i < fire.Length; i++) {
            if (i < this.GetHealth()) {
                fire[i].sprite = fireLit;
            } else {
                fire[i].sprite = fireUnlit;
            }

            if (i < fireVal) {
                fire[i].enabled = true;
            } else {
                fire[i].enabled = false;
            }

        }
        if (this.GetHealth() <= 0) {
            fire[0].sprite = fireUnlit;
        }

    }
    private void FixedUpdate() // call movement every fixed update
    {
        if (canMove)
        {
            Move();
        }
    }
   
    protected override void KillEntity()
    {
        canMove = false;
        canJump = false;
    }
    protected override void Attack() // pretty straight forward, make the player lose a life and attack
    {
        Health--;
        //add shooting
        StartCoroutine(Wait(2));
    }

    void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
        StartCoroutine(Wait(1));
    }

    protected override void Move() // Moves player using rb on z and z axis (not y)
    {
        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 10f, groundLayer, QueryTriggerInteraction.Ignore);

        Vector3 velocity = -rb.velocity;
        velocity.y = 0;

        rb.AddForce(velocity * 0.2f, ForceMode.VelocityChange);

        rb.AddForce(inputVec * speed, ForceMode.VelocityChange);

        transform.up = Vector3.Lerp(transform.up, hit.normal, 5 * Time.fixedDeltaTime);
        Debug.DrawRay(Vector3.one, hit.normal * 5, Color.red);
        Debug.Log("Called");
    }


    private void Interact(Burnable burnable)
    {
        burnable.UseObject(this);
    }

    IEnumerator Wait(float sec)
    {
        canJump = false;
        yield return new WaitForSeconds(sec);
        canJump = true;
    }

}
