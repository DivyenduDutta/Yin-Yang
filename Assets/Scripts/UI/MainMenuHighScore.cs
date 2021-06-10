using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuHighScore : MonoBehaviour
{

    public string levelName;
    public TextMeshProUGUI[] hiScoreTexts;
    public TextMeshProUGUI noHighScoreText;

    private void Start()
    {
        HighScoreData highScoreData = SaveSystem.LoadHighScoreData(levelName);

        int totalScore = 0;
        if (highScoreData == null)
        {
            totalScore = 0;
        }
        else
        {
            for (int i = 0; i < highScoreData.highScores.Length; ++i)
            {
                totalScore += highScoreData.highScores[i];
            }
        }

        if (totalScore == 0)
        {
            noHighScoreText.text = "No High Scores Yet";
        }
        else
        {
            for (int i = 0; i < highScoreData.highScores.Length; ++i)
            {
                if (highScoreData.highScores[i] != 0) {
                    hiScoreTexts[i].text += highScoreData.highScores[i];
                }
                else
                {
                    hiScoreTexts[i].text = "---";
                }
            }
        }
    }
}
