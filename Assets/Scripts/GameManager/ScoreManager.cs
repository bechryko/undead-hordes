using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public void IncrementScore()
    {
        score++;
        UpdateText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = score.ToString();
    }
}
