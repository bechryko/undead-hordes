using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public bool isInvincible = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetAxis("Vertical") < 0 && !isInvincible)
        {
            gameObject.layer = 1;
            isInvincible = true;
            anim.Play("NinjaDodge");
        }
    }

    public void onDodge()
    {
        gameObject.layer = 0;
        isInvincible = false;
    }
}
