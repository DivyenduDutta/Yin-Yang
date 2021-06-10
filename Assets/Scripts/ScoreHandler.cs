using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int score;

    public static ScoreHandler instance;

    public static ScoreHandler GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
