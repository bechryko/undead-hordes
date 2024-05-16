using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTrigger : MonoBehaviour
{
    public void IncrementScore()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().IncrementScore();
    }
}
