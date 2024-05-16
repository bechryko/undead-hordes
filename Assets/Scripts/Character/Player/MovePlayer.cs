using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private const float SPEED = 3f;
    public const bool isSpriteFacingRight = true;

    public bool isFacingRight = isSpriteFacingRight;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private const string throwingAnimationName = "NinjaThrow";
    private const string dodgingAnimationName = "NinjaDodge";
    private const string dyingAnimationName = "NinjaDeath";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        if(IsAnimationName(throwingAnimationName) || IsAnimationName(dyingAnimationName))
        {
            direction = 0f;
        }

        if (IsAnimationName(dodgingAnimationName))
        {
            direction = isFacingRight ? 1f : -1f;
        }
        float movement = direction * SPEED;
        rb.velocity = new Vector2(movement, 0.0f);

        HandleCharacterDirection(direction);
    }

    private void HandleCharacterDirection(float direction)
    {
        if(direction == 0f)
        {
            return;
        }
        bool isFlipped = direction < 0 == isSpriteFacingRight;
        sr.flipX = isFlipped;
        isFacingRight = isSpriteFacingRight && !isFlipped;
    }

    private bool IsAnimationName(string name)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
