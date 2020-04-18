using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Burnable
{
    public Transform respawnPoint;

    public bool isActive = false;
    ParticleSystem fireParticle;
    Light fireLight;
    private void Start() // Set burnable type assign burnable value
    {
        BurnValue = 1;
        objType = BurnableType.Campfire;
        fireParticle = GetComponentInChildren<ParticleSystem>();
        fireLight = GetComponentInChildren<Light>();
    }
    public override void UseObject(PlayerClass player)
    {
        if (isActive)
        {
            UseCampfire(); 
            return; 
        }
        player.TakeDamage(BurnValue);
        isActive = true;
    }

    private void UseCampfire()
    {
        //save players info
        //update checkpoint
        GameManager.instance.CurrentCheckpoint = respawnPoint.position;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            fireLight.enabled = true;
            if (fireParticle.isPaused)
            {
                fireParticle.Play();
            }
        }
        else
        {
            fireLight.enabled = false;
            if (fireParticle.isPlaying)
            {
                fireParticle.Pause();
            }
        }
    }

    public override void UseObject(PlayerClass player, ParticleSystem system, Vector3 pos)
    {
        UseObject(player);
        Instantiate(system, pos, Quaternion.identity);
    }
    public override void DestroyObject()
    {
        Debug.LogWarning("Unintended Object Destroyed" + this.name);
    }
    public override void ResetObject()
    {
        Debug.LogWarning("Unintended Object Used" + this.name);
    }
}
