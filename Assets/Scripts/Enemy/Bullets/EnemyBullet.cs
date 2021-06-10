using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameConstants.Polarity polarity;

    public float BULLET_SPEED;

    public GameConstants.DIRECTION bulletDirection;

    public Vector3 initialBulletDirection; // used only incase of directional guns like on the Boss

    private void Update()
    {
        switch (bulletDirection)
        {
            case GameConstants.DIRECTION.FOLLOW:
                transform.Translate(initialBulletDirection * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.UP: 
                transform.Translate(Vector2.up * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.DOWNLEFT: 
                transform.Translate(new Vector2(-0.5f, -1f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.DOWNRIGHT: 
                transform.Translate(new Vector2(0.5f, -1f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.DOWNLEFT1:
                transform.Translate(new Vector2(-1f, -1f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.DOWNRIGHT1:
                transform.Translate(new Vector2(1f, -1f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.DOWNLEFT2:
                transform.Translate(new Vector2(-0.25f, -1f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.DOWNRIGHT2:
                transform.Translate(new Vector2(0.25f, -1f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.LEFT:
                transform.Translate(new Vector2(-1f, 0f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.RIGHT:
                transform.Translate(new Vector2(1f, 0f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.TOPLEFT:
                transform.Translate(new Vector2(-0.5f, 1f) * Time.deltaTime * BULLET_SPEED);
                break;
            case GameConstants.DIRECTION.TOPRIGHT:
                transform.Translate(new Vector2(0.5f, 1f) * Time.deltaTime * BULLET_SPEED);
                break;
            default: transform.Translate(Vector2.down * Time.deltaTime * BULLET_SPEED);
                break;

        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (ShouldEnemyBulletBeDestroyed(collider.gameObject))
        {
            Destroy(transform.gameObject);
        }
        
    }

    private bool ShouldEnemyBulletBeDestroyed(GameObject gameObject)
    {
        if (gameObject.CompareTag("ship") ||
            gameObject.CompareTag("PlayerDarkBullet") ||
            gameObject.CompareTag("PlayerLightBullet") ||
            gameObject.tag.StartsWith("Enemy") ||
            gameObject.tag.StartsWith("PlayerTakeDmgNoDestroy"))
        {
            return false;
        }
        return true;
    }
}
