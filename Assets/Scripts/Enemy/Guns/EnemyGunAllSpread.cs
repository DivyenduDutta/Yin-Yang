using UnityEngine;
using System;

public class EnemyGunAllSpread : MonoBehaviour
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

        foreach (GameConstants.DIRECTION direction in Enum.GetValues(typeof(GameConstants.DIRECTION)))
        {
            if (direction != GameConstants.DIRECTION.NONE && direction != GameConstants.DIRECTION.FOLLOW) {
                Transform enemyBullet = Instantiate(bulletToShoot, bulletPosition, Quaternion.identity);
                enemyBullet.GetComponent<EnemyBullet>().bulletDirection = direction;
                enemyBullet.GetComponent<EnemyBullet>().BULLET_SPEED = gunBulletSpeed;
            }
        }
    }
}
