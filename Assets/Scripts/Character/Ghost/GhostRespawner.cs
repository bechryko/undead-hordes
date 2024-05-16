using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : Respawner
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Weapon"))
        {
            Transform particles = transform.Find("Particles");
            if (particles) 
            {
                particles.GetComponent<LiveAfterParentDestroy>().AboutToDie();
            }
            GetComponent<ScoreTrigger>().IncrementScore();
            col.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public override void Respawn()
    {
        base.Respawn();
        gameObject.SetActive(false);
    }
}
