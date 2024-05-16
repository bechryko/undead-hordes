using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    private float width;
    private Camera cam;
    public int offset = 10;

    private static List<Tiling> instances;
    private const int MAX_INSTANCES = 2;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        width = Mathf.Abs(spriteRenderer.sprite.bounds.size.x * transform.localScale.x);

        cam = Camera.main;

        instances ??= new List<Tiling>();

        instances.Add(this);

        if (instances.Count < MAX_INSTANCES )
        {
            makeClone();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

        float edgeVisiblePositionRight = (transform.position.x + width / 2) - camHorizontalExtend;
        float edgeVisiblePositionLeft = (transform.position.x - width / 2) + camHorizontalExtend;

        if (cam.transform.position.x >= edgeVisiblePositionRight - offset && this == instances[1])
        {
            Tiling moved = instances[1] = instances[0];
            instances[0] = this;

            moved.GetComponent<Transform>().position += new Vector3(width * 2, 0, 0);
        }
        else if (cam.transform.position.x <= edgeVisiblePositionLeft + offset && this == instances[0])
        {
            Tiling moved = instances[0] = instances[1];
            instances[1] = this;

            moved.GetComponent<Transform>().position -= new Vector3(width * 2, 0, 0);
        }
    }

    void makeClone()
    {
        Vector3 newPos = transform.position + new Vector3(width, 0, 0);

        Transform clone = Instantiate(transform, newPos, transform.rotation);
        clone.parent = transform.parent;
    }
}
