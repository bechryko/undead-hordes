using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private const float SPEED = 35f;
    private const int MAX_OBJECTS = 10;

    public GameObject itemProto;
    private List<GameObject> objectPool;

    private Animator anim;
    private PauseManager pauseManager;

    private const string throwingAnimationName = "NinjaThrow";
    private const string dodgingAnimationName = "NinjaDodge";
    private const string dyingAnimationName = "NinjaDeath";

    void Start()
    {
        objectPool = new List<GameObject>();
        for(int i = 0; i < MAX_OBJECTS; i++)
        {
            GameObject newObject = Instantiate(itemProto);
            newObject.SetActive(false);
            objectPool.Add(newObject);
        }

        anim = GetComponent<Animator>();
        pauseManager = FindAnyObjectByType<PauseManager>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !isAnimationThrowPreventing() && !pauseManager.IsGamePaused())
        {
            anim.Play(throwingAnimationName);
        }
    }

    public void onThrow()
    {
        GameObject newObject = getInactiveObject();
        if (newObject)
        {
            initObject(newObject);
        }
    }

    void initObject(GameObject newObject)
    {
        newObject.transform.position = transform.position;
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        newObject.SetActive(true);
        Physics2D.IgnoreCollision(newObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        bool isFacingRight = GetComponent<MovePlayer>().isFacingRight;
        rb.velocity = new Vector2((isFacingRight ? 1.0f : -1.0f) * SPEED, 0.0f);
        newObject.GetComponent<SpriteRenderer>().flipX = !isFacingRight;
        newObject.GetComponent<DeactivateAfterTime>().StartTimer();
    }

    GameObject getInactiveObject()
    {
        foreach(GameObject go in objectPool)
        {
            if(!go.activeSelf)
            {
                return go;
            }
        }
        return null;
    }

    private bool isAnimationThrowPreventing()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(dodgingAnimationName)
            || anim.GetCurrentAnimatorStateInfo(0).IsName(throwingAnimationName)
            || anim.GetCurrentAnimatorStateInfo(0).IsName(dyingAnimationName);
    }
}
