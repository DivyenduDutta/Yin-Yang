using System;
using UnityEngine;


public class BossEvent : MonoBehaviour
{
    public Animator animator;
    private bool checkStart;

    public static event EventHandler BossDied;

    public GameObject bossPhase2Left;
    public GameObject bossPhase2Right;

    private void Start()
    {
        checkStart = false;
    }

    private void Update()
    {
        if (!transform.Find("SubEvent1"))
        {
            //trigger next sub event instantly
            TriggerBossEventPhase2();
        }

        if (checkStart && !GameObject.Find("LeftGuns") && !GameObject.Find("RightGuns"))
        {
            //here its gameover as in boss is dead
            //LevelHandler.GetInstance().isGameOver = true;
            AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDeath, 0.1f);

            BossDied?.Invoke(this, EventArgs.Empty);
            Destroy(bossPhase2Left);
            Destroy(bossPhase2Right);

            Vector3 pos5 = new Vector3(transform.position.x - 2.0f, transform.position.y + 4.0f, transform.position.z - 10);
            Transform par5 = Instantiate(GameAssets.GetInstance().enemyDamageParSys, pos5, Quaternion.identity);
            HandleEnemyDeathEffectDuration(par5, 0f);

            Vector3 pos6 = new Vector3(transform.position.x + 2.0f, transform.position.y + 5.5f, transform.position.z - 10);
            Transform par6 = Instantiate(GameAssets.GetInstance().enemyDamageParSys, pos6, Quaternion.identity);
            HandleEnemyDeathEffectDuration(par6, 0.2f);
        }
    }

    private void HandleEnemyDeathEffectDuration(Transform deathEffectParSys, float delay)
    {
        ParticleSystem ps = deathEffectParSys.GetComponent<ParticleSystem>();
        ps.Stop();

        var main = ps.main;
        main.duration = 1.0f;
        main.startDelay = delay;

        ps.Play();
    }

    private void TriggerBossEventPhase2()
    {
        if (!animator.GetBool("Boss_P2_main"))
        {
            animator.SetBool("Boss_P2_main", true);
        }
    }

    public void EnableCheckForBossDeath()
    {
        checkStart = true;
    }

    public void TriggerBossMainAnim()
    {
        if (!animator.GetBool("Boss_Anim"))
        {
            animator.SetBool("Boss_Anim", true);
        }
    }
}
