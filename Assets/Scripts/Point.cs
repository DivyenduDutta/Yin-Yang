using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameConstants.Polarity polarity;
    public int pointAmt;

    private bool shouldBeMultiplier; //sometimes points may go away without it being hit by player bullet
    //private bool shouldBeDestroyed;

    private void Awake()
    {
        shouldBeMultiplier = false;
        //shouldBeDestroyed = false;
    }

    private void Update()
    {
        /*if (shouldBeMultiplier)
        {
            //ScoreHandler.GetInstance().AddToScore(45);
            Instantiate(GameAssets.GetInstance().pointMultiplier, transform.position, Quaternion.identity);
            shouldBeMultiplier = false;
        }*/
        /*if (shouldBeDestroyed)
        {
            Destroy(transform.gameObject);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (polarity == GameConstants.Polarity.dark)
        {
            if (collider.gameObject.tag == "PlayerDarkBullet")
            {
                shouldBeMultiplier = true;
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            if (collider.gameObject.tag == "PlayerLightBullet")
            {
                shouldBeMultiplier = true;
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        /*if (collider.gameObject.tag.Contains("Bullet"))
        {
            shouldBeDestroyed = true;
        }*/
    }

    public void DestroyPointAfterAnimation()
    {
        if (shouldBeMultiplier)
        {
            //ScoreHandler.GetInstance().AddToScore(45);
            Instantiate(GameAssets.GetInstance().pointMultiplier, transform.position, Quaternion.identity)
                .GetComponent<PointMultiplier>().pointAmt = pointAmt;
            //shouldBeMultiplier = false;
        }
        //shouldBeDestroyed = true;
        Destroy(transform.gameObject);
    }
}
