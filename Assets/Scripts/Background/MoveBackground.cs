using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private const float SPEED = 0.25f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        float velocity = Input.GetAxis("Horizontal") * -1 * SPEED;

        Vector3 pos = transform.position;
        pos.x += velocity * Time.deltaTime;
        transform.position = pos;
    }
}
