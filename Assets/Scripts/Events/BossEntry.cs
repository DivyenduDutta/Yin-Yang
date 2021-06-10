using System;
using UnityEngine;
using UnityEngine.Playables;

public class BossEntry : MonoBehaviour
{
    public PlayableDirector bossEntryDirector;
    public PlayableDirector bossExitDirector;

    public GameObject koel;

    public GameObject stageClear;

    private void Awake()
    {
        ParallelEnemyEvent.BossEntryEvent += BosSEntry_BossEntryEvent;
        BossEvent.BossDied += BossEvent_BossExitEvent;
    }

    private void OnDestroy()
    {
        ParallelEnemyEvent.BossEntryEvent -= BosSEntry_BossEntryEvent;
        BossEvent.BossDied -= BossEvent_BossExitEvent;
    }

    private void BosSEntry_BossEntryEvent(object sender, EventArgs e)
    {
        Invoke("BossEntryCutscene", 5.0f);
    }

    private void BossEvent_BossExitEvent(object sender, EventArgs e)
    {
        Invoke("BossExitCutscene", 5.0f);
    }

    private void BossEntryCutscene()
    {
        if (!LevelHandler.GetInstance().isGameOver)
        {
            MainGameAudio.GetInstance().FadeGameAudio();
            MainGameAudio.GetInstance().PlayBossAudio();
            bossEntryDirector.Play();
        }
    }

    private void BossExitCutscene()
    {
        LevelHandler.GetInstance().isGameOver = true;
        bossExitDirector.Play();
        koel.GetComponentInChildren<Gun>().StopShoot();
    }

    public void EnableStageCLearScreen()
    {
        MainGameAudio.GetInstance().FadeGameAudio();
        stageClear.SetActive(true);
    }
}
