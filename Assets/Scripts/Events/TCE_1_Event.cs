using UnityEngine;

public class TCE_1_Event : MonoBehaviour
{
    private void Update()
    {
        if (!(transform.Find("TCE_side_light") || transform.Find("TCE_side_dark")))
        {
            AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDeath, 1.0f);

            Vector3 pos1 = new Vector3(transform.position.x - 3.0f, transform.position.y, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
            Vector3 pos3 = new Vector3(transform.position.x + 3.0f, transform.position.y, transform.position.z);
            Vector3 pos4 = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
            Transform par1 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos1, Quaternion.identity);
            HandleEnemyDeathEffectDuration(par1, 0f);

            Transform par2 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos2, Quaternion.identity);
            HandleEnemyDeathEffectDuration(par2, 0.4f);

            Transform par3 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos3, Quaternion.identity);
            HandleEnemyDeathEffectDuration(par3, 0.2f);

            Transform par4 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos4, Quaternion.identity);
            HandleEnemyDeathEffectDuration(par4, 0.6f);
            Destroy(transform.gameObject);
        }
    }

    private void HandleEnemyDeathEffectDuration(Transform deathEffectParSys, float delay)
    {
        ParticleSystem ps = deathEffectParSys.GetComponent<ParticleSystem>();
        ps.Stop();

        var main = ps.main;
        main.duration = 2.0f;
        main.startDelay = delay;

        ps.Play();
    }
}
