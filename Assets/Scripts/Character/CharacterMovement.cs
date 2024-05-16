using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private const string movementPropKey = "isMoving";

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool(movementPropKey, Math.Abs(rb.velocity.x) > 0f);
    }
}
