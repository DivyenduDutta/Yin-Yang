using UnityEngine;

public class EnemyGunSpread : MonoBehaviour
{
    public GameConstants.Polarity polarity;

    public float enemyGunHealth;

    public int scoreToAdd;

    public float gunBulletSpeed;

    public bool canGunBeDestroyed;

    private void Awake()
    {
        InvokeRepeating("Shoot", 0.5f, 0.5f);
        if (!canGunBeDestroyed)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Shoot()
    {
        Transform bulletToShoot;
        if (polarity == GameConstants.Polarity.light)
        {
            bulletToShoot = GameAssets.GetInstance().whiteEnemyBullet;
        }
        else
        {
            bulletToShoot = GameAssets.GetInstance().blackEnemyBullet;
        }
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Transform enemyBullet1 = Instantiate(bulletToShoot, bulletPosition, Quaternion.identity);
        enemyBullet1.GetComponent<EnemyBullet>().bulletDirection = GameConstants.DIRECTION.DOWNLEFT;
        enemyBullet1.GetComponent<EnemyBullet>().BULLET_SPEED = gunBulletSpeed;

        Transform enemyBullet2 = Instantiate(bulletToShoot, bulletPosition, Quaternion.identity);
        enemyBullet2.GetComponent<EnemyBullet>().bulletDirection = GameConstants.DIRECTION.DOWNRIGHT;
        enemyBullet2.GetComponent<EnemyBullet>().BULLET_SPEED = gunBulletSpeed;

        Transform enemyBullet3 = Instantiate(bulletToShoot, bulletPosition, Quaternion.identity);
        enemyBullet3.GetComponent<EnemyBullet>().bulletDirection = GameConstants.DIRECTION.DOWN;
        enemyBullet3.GetComponent<EnemyBullet>().BULLET_SPEED = gunBulletSpeed;
    }
}
