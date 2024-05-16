using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : Respawner
{
    private Animator anim;
    private Dodge dodge;

    private const string deathAnimationName = "NinjaDeath";
    private const string idleAnimationName = "NinjaIdle";

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        dodge = GetComponent<Dodge>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && !dodge.isInvincible)
        {
            anim.Play(deathAnimationName);
        }
    }

    public void onDeath()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().reset();
    }

    public override void Respawn()
    {
        base.Respawn();
        anim.Play(idleAnimationName);
    }

}
