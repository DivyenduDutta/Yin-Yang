using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{

    public static LevelHandler instance;

    public static LevelHandler GetInstance()
    {
        return instance;
    }

    private Queue<GameEventHandler> gameEventHandlerQueue;

    public GameObject foregroundClouds;

    public bool isGameOver;
    private HighScoreData highScoreData;

    public GameObject gameOverScreen;

    private void Awake()
    {
        instance = this;
        gameEventHandlerQueue = new Queue<GameEventHandler>();
        isGameOver = false;
        BossEvent.BossDied += LevelHandler_BossDied;
        Koel.KoelDied += LevelHandler_KoelDied;

        LoadHighScores();
    }

    private void OnDestroy()
    {
        BossEvent.BossDied -= LevelHandler_BossDied;
        Koel.KoelDied -= LevelHandler_KoelDied;
    }
    private void Update()
    {
        if (gameEventHandlerQueue.Count > 0)
        {
            GameEventHandler currentGameEventHandler = gameEventHandlerQueue.Dequeue();
            Debug.Log("Invoking event: " + currentGameEventHandler.gameEvent.ToString() + " in: " + currentGameEventHandler.delay);
            Invoke(currentGameEventHandler.eventHandlerName, currentGameEventHandler.delay);
        }
    }

    public void AddEventToQueue(GameConstants.GameEvents gameEvent, string eventHandlerName, float delay)
    {
        GameEventHandler newGameEvent = new GameEventHandler(gameEvent, eventHandlerName, delay);
        gameEventHandlerQueue.Enqueue(newGameEvent);
    }

    private void StartGameEvent1()
    {
        GameAssets.GetInstance().gameEvent1.SetActive(true);
    }

    private void StartPreGameEvent1Tutorial()
    {
        GameAssets.GetInstance().preGameEvent1Tutorial.SetActive(true);
    }

    private void StartCurtainEnemyEvent()
    {
        GameAssets.GetInstance().curtainEnemyEvent.SetActive(true);
    }

    private void StartParallelEnemyEvent()
    {
        GameAssets.GetInstance().parallelEnemyEvent.SetActive(true);
    }

    private void StartChangeBGEvent()
    {
        
        foregroundClouds.SetActive(true);
        /*infiniteBG.SetActive(false);
        infiniteBGIntense.SetActive(true);
        Destroy(dummyChangeBGEvent);*/
    }

    private void StartBossEvent()
    {
        GameAssets.GetInstance().bossEnemyEvent.SetActive(true);
    }

    private void LoadHighScores()
    {
        highScoreData = SaveSystem.LoadHighScoreData(SceneManager.GetActiveScene().name);
        if (highScoreData == null || highScoreData.highScores == null || highScoreData.highScores.Length == 0)
        {
            highScoreData = new HighScoreData(SceneManager.GetActiveScene().name);
        }
    }

    private void LevelHandler_BossDied(object sender, EventArgs e)
    {
        CheckAndAddToHighScore();
    }

    private void LevelHandler_KoelDied(object sender, EventArgs e)
    {
        MainGameAudio.GetInstance().FadeGameAudio();
        CheckAndAddToHighScore();
        gameOverScreen.SetActive(true);
    }

    private void CheckAndAddToHighScore()
    {
        if (isGameOver)
        {
            //PrintHighScores();
            int currentRunScore = ScoreHandler.GetInstance().score;
            //add to highscores
            for (int i=0; i<highScoreData.highScores.Length; ++i)
            {
                if (currentRunScore > highScoreData.highScores[i])
                {
                    if (highScoreData.highScores[i] == 0)
                    {
                        highScoreData.highScores[i] = currentRunScore;
                    }
                    else
                    {
                        MoveScoresDown(i);
                        highScoreData.highScores[i] = currentRunScore;
                    }
                    break;
                } 
                else if (currentRunScore == highScoreData.highScores[i])
                {
                    break; //dont write same score twice
                }
            }

            //save to file
            SaveSystem.SaveHighScoreData(SceneManager.GetActiveScene().name, highScoreData.highScores);
        }
    }

    private void MoveScoresDown(int indexToMoveDownFrom)
    {
        if (indexToMoveDownFrom == (highScoreData.highScores.Length - 1))
        {
            return;
        }

        for (int i = (highScoreData.highScores.Length-2); i >= indexToMoveDownFrom; --i)
        {
            highScoreData.highScores[i + 1] = highScoreData.highScores[i];
        }
    }

    private void PrintHighScores()
    {
        Debug.Log("Displaying high scores");
        for (int i=0; i< highScoreData.highScores.Length; ++i)
        {
            Debug.Log(highScoreData.highScores[i]);
        }
        Debug.Log("Done...");
    }
}
