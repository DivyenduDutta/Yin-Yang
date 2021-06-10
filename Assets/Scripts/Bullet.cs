using UnityEngine;

public class Bullet : MonoBehaviour
{

    private const float BULLET_SPEED = 30f;

    public float BULLET_DAMAGE = 5.0f;
    public GameConstants.Polarity bulletPolarity;

    public int bulletIndex;

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * BULLET_SPEED);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        //DebugScript.GetInstance().myName = collider.gameObject.name;
        /*if (!(collider.gameObject.tag.EndsWith("Bullet") || collider.gameObject.name.Contains("Point"))
            && ShouldBulletBeDestroyed(collider))*/
        if (!(collider.gameObject.tag.EndsWith("Bullet") || collider.gameObject.name.Contains("Point")
            || (collider.gameObject.CompareTag("EnemyDarkBullet")) || (collider.gameObject.CompareTag("EnemyLightBullet"))))
        {
            //dont destroy bullets if they collide with themselves or points
            //DebugScript.GetInstance().myName = collider.gameObject.name;
            //Destroy(transform.gameObject);

            Gun.GetInstance().DeactivateBulletAfterDestroy(bulletPolarity, bulletIndex);
        }
    }

    private bool ShouldBulletBeDestroyed(Collision2D collider)
    {
        //opposite color bullets will not hit enemies
        if ((transform.gameObject.CompareTag("PlayerLightBullet")
            && collider.gameObject.CompareTag("EnemyDark"))
            || 
            (transform.gameObject.CompareTag("PlayerDarkBullet")
            && collider.gameObject.CompareTag("EnemyLight")))
        {
            return false;
        }
        return true;
    }
}
