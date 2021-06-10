using UnityEngine;

public class EnemyGunRotate : MonoBehaviour
{
    private const int ROTATION_START = 10;
    private const int ROTATION_END = 50;

    private const float ROTATION_AMT = 10.0f;
    private const float angle = 20f;

    private float multiplier;

    private SpriteRenderer gunSpriteRenderer;

    private void Awake()
    {
        InvokeRepeating("RotateGun", 0.5f, 0.5f);
        multiplier = 1.0f;
        gunSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void RotateGun()
    {
        
        if (transform.parent.name == "LeftGuns")
        {
            //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + (ROTATION_AMT * multiplier));
            transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward * multiplier);
        }
        else
        {
            //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - (ROTATION_AMT * multiplier));
            transform.rotation *= Quaternion.AngleAxis(angle,  Vector3.forward * multiplier);
        }

        if ((int)Mathf.Abs(transform.eulerAngles.z) >= ROTATION_END && multiplier != -1f)
        {
            multiplier = -1f;
        }
        if ((int)Mathf.Abs(transform.eulerAngles.z) == ROTATION_START && multiplier != 1f)
        {
            multiplier = 1f;
            if (GetComponent<EnemyGun>().polarity == GameConstants.Polarity.light)
            {
                gunSpriteRenderer.sprite = GameAssets.GetInstance().bossGunDark;
                GetComponent<EnemyGun>().polarity = GameConstants.Polarity.dark;
            }
            else
            {
                gunSpriteRenderer.sprite = GameAssets.GetInstance().bossGunLight;
                GetComponent<EnemyGun>().polarity = GameConstants.Polarity.light;
            }
            
        }
    }
}
