using UnityEngine;
using System;

public class TutorialTextEnabler : MonoBehaviour
{
    //public static event EventHandler OnTutorialBullet;
    public GameObject tutorialText1;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if ((transform.gameObject.GetComponent<Enemy>().polarity == GameConstants.Polarity.dark)
            && (collider.gameObject.tag.Equals("PlayerLightBullet")))
        {
            tutorialText1.SetActive(true);
            //OnTutorialBullet?.Invoke(this, EventArgs.Empty);
        }

        if ((transform.gameObject.GetComponent<Enemy>().polarity == GameConstants.Polarity.light)
            && (collider.gameObject.tag.Equals("PlayerDarkBullet")))
        {
            tutorialText1.SetActive(true);
            //OnTutorialBullet?.Invoke(this, EventArgs.Empty);
        }
    }

    
}
