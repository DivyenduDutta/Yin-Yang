using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    /*private TextMeshProUGUI tutorialTextComp;
    public string tutText;*/

    public Animator animator;
    public float disableTutorialOffset;

    private void Update()
    {
        if (LevelHandler.GetInstance().isGameOver)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Invoke("DisableText", disableTutorialOffset);
        //TutorialTextEnabler.OnTutorialBullet += Tutorial_OnTutorialBullet;
    }

    /*void OnDestroy()
    {
        TutorialTextEnabler.OnTutorialBullet -= Tutorial_OnTutorialBullet;
    }*/

    /*private void Tutorial_OnTutorialBullet(object sender, System.EventArgs e)
    {
        tutorialTextComp.text = tutText;
        Invoke("DisableText", 2f);
    }*/

    private void DisableText()
    {
        animator.SetTrigger("TutorialTextExit");
    }

    public void DisableTutorialGameObj()
    {
        this.transform.gameObject.SetActive(false);
    }
}
