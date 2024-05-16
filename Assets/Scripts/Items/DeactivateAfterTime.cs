using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    public float time;
    private float remainingTime;

    public void StartTimer()
    {
        remainingTime = time;
    }

    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
