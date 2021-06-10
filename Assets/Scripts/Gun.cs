using UnityEngine;

public class Gun : MonoBehaviour
{

    private GameObject[] whiteBullets;
    private GameObject[] blackBullets;

    private const int NUMBER_OF_BULLETS = 8;
    private int whiteBulletIndex;
    private int blackBulletIndex;

    public static Gun instance;

    public static Gun GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;

        whiteBulletIndex = 0;
        blackBulletIndex = 0;

        InitializePlayerBullets();

        InvokeRepeating("Shoot", 1.2f, 0.1f);    
    }

    /*private void Shoot()
    {
        Transform bulletToShoot;
        if (transform.parent.gameObject.GetComponent<Koel>().shipPolarity == GameConstants.Polarity.light)
        {
            bulletToShoot = GameAssets.GetInstance().whiteBullet;
        }
        else
        {
            bulletToShoot = GameAssets.GetInstance().blackBullet;
        }
        Vector3 leftBulletPosition = new Vector3(transform.position.x - 0.15f, transform.position.y + 1.3f, transform.position.z);
        Vector3 rightBulletPosition = new Vector3(transform.position.x + 0.15f, transform.position.y + 1.3f, transform.position.z);

        Instantiate(bulletToShoot, leftBulletPosition, Quaternion.identity);
        Instantiate(bulletToShoot, rightBulletPosition, Quaternion.identity);
    }*/

    private void Shoot()
    {
        Vector3 leftBulletPosition = new Vector3(transform.position.x - 0.15f, transform.position.y + 1.3f, transform.position.z);
        Vector3 rightBulletPosition = new Vector3(transform.position.x + 0.15f, transform.position.y + 1.3f, transform.position.z);
        if (transform.parent.gameObject.GetComponent<Koel>().shipPolarity == GameConstants.Polarity.light)
        {
            whiteBullets[whiteBulletIndex].GetComponent<Transform>().position = leftBulletPosition;
            whiteBullets[whiteBulletIndex].SetActive(true);

            whiteBullets[whiteBulletIndex + 1].GetComponent<Transform>().position = rightBulletPosition;
            whiteBullets[whiteBulletIndex + 1].SetActive(true);

            whiteBulletIndex = (whiteBulletIndex + 2) % NUMBER_OF_BULLETS;
        }
        else
        {
            blackBullets[blackBulletIndex].GetComponent<Transform>().position = leftBulletPosition;
            blackBullets[blackBulletIndex].SetActive(true);

            blackBullets[blackBulletIndex + 1].GetComponent<Transform>().position = rightBulletPosition;
            blackBullets[blackBulletIndex + 1].SetActive(true);

            blackBulletIndex = (blackBulletIndex + 2) % NUMBER_OF_BULLETS;
        }
    }

    private void InitializePlayerBullets()
    {
        whiteBullets = new GameObject[NUMBER_OF_BULLETS];
        blackBullets = new GameObject[NUMBER_OF_BULLETS];

        Transform whiteBullet = GameAssets.GetInstance().whiteBullet;
        Transform blackBullet = GameAssets.GetInstance().blackBullet;

        for (int i = 0; i < NUMBER_OF_BULLETS; i+=2)
        {
            Vector3 initialLeftBulletPosition = new Vector3(-1, 0, 0);
            Vector3 initialRightBulletPosition = new Vector3(1, 0, 0);

            whiteBullets[i] = Instantiate(whiteBullet, initialLeftBulletPosition, Quaternion.identity).gameObject;
            whiteBullets[i].SetActive(false);
            whiteBullets[i].GetComponent<Bullet>().bulletIndex = i;

            blackBullets[i] = Instantiate(blackBullet, initialLeftBulletPosition, Quaternion.identity).gameObject;
            blackBullets[i].SetActive(false);
            blackBullets[i].GetComponent<Bullet>().bulletIndex = i;

           
            whiteBullets[i+1] = Instantiate(whiteBullet, initialRightBulletPosition, Quaternion.identity).gameObject;
            whiteBullets[i+1].SetActive(false);
            whiteBullets[i+1].GetComponent<Bullet>().bulletIndex = i+1;

            blackBullets[i+1] = Instantiate(blackBullet, initialRightBulletPosition, Quaternion.identity).gameObject;
            blackBullets[i+1].SetActive(false);
            blackBullets[i+1].GetComponent<Bullet>().bulletIndex = i+1;

        }
    }

    public void DeactivateBulletAfterDestroy(GameConstants.Polarity polarity, int bulletIndex)
    {
        if (polarity == GameConstants.Polarity.light)
        {
            whiteBullets[bulletIndex].SetActive(false);
        }
        else
        {
            blackBullets[bulletIndex].SetActive(false);
        }
    }

    public void StopShoot()
    {
        CancelInvoke("Shoot");
    }
}
