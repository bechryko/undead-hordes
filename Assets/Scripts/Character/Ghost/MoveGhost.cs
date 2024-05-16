using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveGhost : MonoBehaviour
{
    private const float SPEED = 1.5f;
    public const bool isSpriteFacingRight = true;
    private const float POSITION_MATCH_THRESHOLD = 0.1f;
    private const float GUARD_PERCENT_CHANCE = 33.3f;
    private const float MIN_GUARD_TIME = 2.5f;
    private const float MAX_GUARD_TIME = 6.0f;
    private const float MIN_NEW_TARGET_DISTANCE = 1.5f;
    private const float MAX_NEW_TARGET_DISTANCE = 3.5f;
    private const string GUARD_ANIMATION_NAME = "GhostIdle";

    public bool isFacingRight = isSpriteFacingRight;
    private float targetX;
    private float guardTime = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        ReplanTargetPosition(false);
    }

    void Update()
    {
        guardTime = Math.Max(0, guardTime - Time.deltaTime);
        if(guardTime > 0)
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
            return;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(GUARD_ANIMATION_NAME))
        {
            anim.SetBool("IsGuarding", false);
        }

        float direction = targetX > transform.position.x ? 1 : -1;
        float movement = direction * SPEED;
        rb.velocity = new Vector2(movement, 0.0f);

        HandleCharacterDirection(direction);

        CheckTargetPosition();
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

    private void CheckTargetPosition()
    {
        if(Math.Abs(transform.position.x - targetX) < POSITION_MATCH_THRESHOLD)
        {
            ReplanTargetPosition();
        }
    }

    public void ReplanTargetPosition(bool canGuard = true)
    {
        bool doGuard = UnityEngine.Random.Range(0, 100) < GUARD_PERCENT_CHANCE;
        if(doGuard && canGuard) {
            guardTime = UnityEngine.Random.Range(MIN_GUARD_TIME, MAX_GUARD_TIME);
            anim.SetBool("IsGuarding", true);
            return;
        }

        float direction = UnityEngine.Random.Range(0, 2) * 2 - 1;
        float distance = UnityEngine.Random.Range(MIN_NEW_TARGET_DISTANCE, MAX_NEW_TARGET_DISTANCE);
        targetX = transform.position.x + direction * distance;
    }
}
