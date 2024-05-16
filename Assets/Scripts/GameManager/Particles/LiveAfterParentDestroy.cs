using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveAfterParentDestroy : MonoBehaviour
{
    public void AboutToDie()
    {
        transform.parent = null;
        GetComponent<ParticleSystem>().Stop();
    }
}
