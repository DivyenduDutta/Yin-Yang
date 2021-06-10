using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameConstants.Polarity polarity;

    public float enemyGunHealth;

    public int scoreToAdd;

    public GameConstants.DIRECTION gunShootDirection;
    public float gunBulletSpeed;

    public bool canGunBeDestroyed;
    public float gunShootStart;
    public float gunShootInterval = 0.5f;

    public bool isOffsetNeeded = true; //for offsetting the bullet spawn postion from the gun

    public int bulletType = 1;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        InvokeRepeating("Shoot", gunShootStart, gunShootInterval);
    }

    private void Update()
    {
        if (enemyGunHealth <= 0f)
        {
            //AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDeath, 1.0f);
            ScoreHandler.GetInstance().AddToScore(scoreToAdd);
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
                enemyGunHealth -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE * 0.0f; //no damage
            }
            else if (collider.gameObject.tag == "PlayerDarkBullet")
            {
                enemyGunHealth -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE;
                
                Instantiate(GameAssets.GetInstance().enemyDamageParSys, parPosition, Quaternion.identity).parent = transform;

                AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDestroy, 0.4f);
            }
        }
        else
        {
            if (collider.gameObject.tag == "PlayerLightBullet")
            {
                enemyGunHealth -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE;
                Instantiate(GameAssets.GetInstance().enemyDamageParSys, parPosition, Quaternion.identity).parent = transform;

                AudioHandler.PlayAudio(AudioHandler.Sound.EnemyDestroy, 0.4f);
            }
            else if (collider.gameObject.tag == "PlayerDarkBullet")
            {
                enemyGunHealth -= collider.gameObject.GetComponent<Bullet>().BULLET_DAMAGE * 0.0f; //no damage
            }
        }
    }

    private void Shoot()
    {
        if (canGunBeDestroyed)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        Transform bulletToShoot;
        switch (bulletType)
        {
            case 1:
                if (polarity == GameConstants.Polarity.light)
                {
                    bulletToShoot = GameAssets.GetInstance().whiteEnemyBullet;
                }
                else
                {
                    bulletToShoot = GameAssets.GetInstance().blackEnemyBullet;
                }
                break;
            case 2:
                if (polarity == GameConstants.Polarity.light)
                {
                    bulletToShoot = GameAssets.GetInstance().whiteEnemyBulletType2;
                }
                else
                {
                    bulletToShoot = GameAssets.GetInstance().blackEnemyBulletType2;
                }
                break;
            default:
                if (polarity == GameConstants.Polarity.light)
                {
                    bulletToShoot = GameAssets.GetInstance().whiteEnemyBullet;
                }
                else
                {
                    bulletToShoot = GameAssets.GetInstance().blackEnemyBullet;
                }
                break;
        }

        
        float offset = 1f;
        if (!isOffsetNeeded)
        {
            offset = 0f;
        }
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);

        Transform enemyBullet = Instantiate(bulletToShoot, bulletPosition, Quaternion.identity);
        enemyBullet.GetComponent<EnemyBullet>().bulletDirection = gunShootDirection;
        enemyBullet.GetComponent<EnemyBullet>().BULLET_SPEED = gunBulletSpeed;
        enemyBullet.GetComponent<EnemyBullet>().initialBulletDirection = -1.0f * transform.up;
        //enemyBullet.parent = transform;
    }
}
