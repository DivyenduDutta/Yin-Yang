using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverText : MonoBehaviour
{
    public TextMeshProUGUI finalScore;

    private void Update()
    {
        finalScore.text = ScoreHandler.GetInstance().score.ToString();
    }

    public void OnClickRetry()
    {
        PlayerPrefs.SetInt("shouldCutsceneBePlayed", 0);
        LevelLoader.GetInstance().Load((LevelLoader.Scene)Enum.Parse(typeof(LevelLoader.Scene),  SceneManager.GetActiveScene().name));
    }

    public void OnClickMainMenu()
    {
        LevelLoader.GetInstance().Load(LevelLoader.Scene.MainMenu);
    }

}
