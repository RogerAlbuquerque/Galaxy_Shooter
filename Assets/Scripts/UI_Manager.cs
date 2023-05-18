using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public int score;


    public void UpdateLives(sbyte currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore(byte extraScore = 0)
    {
        score = score + 10 + extraScore;
        scoreText.text = "Score: " + score;
    }

    public void resetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }



}
