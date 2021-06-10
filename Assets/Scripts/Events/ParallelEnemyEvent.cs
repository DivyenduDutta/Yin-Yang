using System;
using UnityEngine;

public class ParallelEnemyEvent : MonoBehaviour
{
    public GameObject bottomGunLight;
    public GameObject bottomGunDark;

    public GameObject subEvent1;

    public static event EventHandler BossEntryEvent;

    private bool checkForNextEvent = true;

    private void Update()
    {
        if (subEvent1 == null && checkForNextEvent)
        {
            AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDeath, 1.0f);

            Destroy(bottomGunLight);
            Destroy(bottomGunDark);

            BossEntryEvent?.Invoke(this, EventArgs.Empty);
            checkForNextEvent = false;
        }
    }

}


