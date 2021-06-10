using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{

    public GameConstants.GameEvents nextEvent;
    public string nextEventHandler;
    public float delay;

    //public GameObject gb;
    private void Update()
    {
        if (LevelHandler.GetInstance().isGameOver)
        {
            Destroy(transform.gameObject);
        }

        if(transform.childCount <= 0)
        {
            //all children destroyed
            //gb.SetActive(true);
            EndGameEvent();
        }
    }
    public void EndGameEvent()
    {
        Destroy(transform.gameObject);
        StartNextEvent();
    }

    public void StartNextEvent()
    {
        LevelHandler
            .GetInstance()
            .AddEventToQueue(nextEvent, nextEventHandler, delay);
    }
}
