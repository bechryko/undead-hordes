using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    private Vector2 original_position;
    private bool original_active;

    private RespawnManager rm;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        original_position = transform.position;
        original_active = gameObject.activeSelf;

        rm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>();
        rm.register(this);
    }

    public virtual void Respawn() {
        transform.position = original_position;
        gameObject.SetActive(original_active);
    }
}
