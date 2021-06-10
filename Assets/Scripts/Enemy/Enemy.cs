using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameConstants.Polarity polarity;

    public float health;

    public int scoreToAdd;

    public GameConstants.EnemyTypes enemyType;

    public bool needsRotation = false;
    private float multiplier = -1f;

    private void Update()
    {
        if (needsRotation) {
            Vector3 shipPosition = Koel.GetInstance().transform.position;
            float xAmt = transform.position.x - shipPosition.x;
            float yAmt = transform.position.y - shipPosition.y;
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(xAmt, yAmt) * Mathf.Rad2Deg,
                                    Vector3.forward * multiplier);
        }


        if (health <= 0f)
        {
            ScoreHandler.GetInstance().AddToScore(scoreToAdd);

            HandleDeathEffectBasedOnEnemyType(enemyType);

            //AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDestroy, 1.0f);

            Transform point = null;
            if (polarity == GameConstants.Polarity.light)
            {
                point = Instantiate(GameAssets.GetInstance().pointDark, transform.position, Quaternion.identity);
            }
            else
            {
                point = Instantiate(GameAssets.GetInstance().pointLight, transform.position, Quaternion.identity);
            }

            point.GetComponent<Point>().pointAmt = scoreToAdd;
            SetPointSprite(point, polarity);
            Destroy(transform.gameObject);
        }
    }

    private void HandleDeathEffectBasedOnEnemyType(GameConstants.EnemyTypes enemyType)
    {
        switch (enemyType)
        {
            case GameConstants.EnemyTypes.Basic:
                Instantiate(GameAssets.GetInstance().enemyDeathParSys, transform.position, Quaternion.identity);
                break;
            case GameConstants.EnemyTypes.EnemyType2:
            case GameConstants.EnemyTypes.BossP1:
                Vector3 pos1 = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
                Vector3 pos2 = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
                Vector3 pos3 = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
                Vector3 pos4 = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
                Transform par1 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos1, Quaternion.identity);
                HandleEnemyDeathEffectDuration(par1, 0f);

                Transform par2 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos2, Quaternion.identity);
                HandleEnemyDeathEffectDuration(par2, 0.4f);

                Transform par3 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos3, Quaternion.identity);
                HandleEnemyDeathEffectDuration(par3, 0.2f);

                Transform par4 = Instantiate(GameAssets.GetInstance().enemyDeathParSys, pos4, Quaternion.identity);
                HandleEnemyDeathEffectDuration(par4, 0.6f);
                break;
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

    private void SetPointSprite(Transform point, GameConstants.Polarity polarity)
    {
        switch (scoreToAdd)
        {
            case 5:
                if (polarity == GameConstants.Polarity.dark)
                {
                    point.GetComponent<SpriteRenderer>().sprite = GameAssets.GetInstance().point_5_dark;
                }
                else
                {
                    point.GetComponent<SpriteRenderer>().sprite = GameAssets.GetInstance().point_5_light;
                }
                break;
            case 20:
                if (polarity == GameConstants.Polarity.dark)
                {
                    point.GetComponent<SpriteRenderer>().sprite = GameAssets.GetInstance().point_20_dark;
                }
                else
                {
                    point.GetComponent<SpriteRenderer>().sprite = GameAssets.GetInstance().point_20_light;
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        Vector3 parPosition = new Vector3(transform.position.x,
                                         transform.position.y - (transform.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f),
                                         transform.position.z-10);

        if (polarity == GameConstants.Polarity.dark)
        {
            if (collider.gameObject.tag == "PlayerLightBullet")
            {
                health -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE * 0.0f; //no damage
            }
            else if (collider.gameObject.tag == "PlayerDarkBullet")
            {
                health -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE;
                Instantiate(GameAssets.GetInstance().enemyDamageParSys, parPosition, Quaternion.identity).parent = transform;

                AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDestroy, 0.4f);
            }
        }
        else
        {
            if (collider.gameObject.tag == "PlayerLightBullet")
            {
                health -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE;
                Instantiate(GameAssets.GetInstance().enemyDamageParSys, parPosition, Quaternion.identity).parent = transform;

                AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDestroy, 0.4f);
            }
            else if (collider.gameObject.tag == "PlayerDarkBullet")
            {
                health -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE * 0.0f; //no damage
            }
        }

        if (collider.gameObject.tag.Equals("DestructionLimit"))
        {
            Destroy(transform.gameObject);
        }
    }
}
