using UnityEngine;

public class TheCurtainEnemyEvent : MonoBehaviour
{
    public Animator tce1Animator;
    public GameObject tutorialText;

    private void Awake()
    {
        Invoke("ShowTutorialText", 3f);

        tutorialText.GetComponent<Animator>().SetTrigger("TutorialTextExit");
        Invoke("StartMoveHorizontal", 10f);
        InvokeRepeating("SpawnEnemyType1Horizontal", 15f, 3f);
    }

    private void StartMoveHorizontal()
    {
        tce1Animator.SetBool("TCEMoveHorizontal", true);
    }

    private void ShowTutorialText()
    {
        tutorialText.SetActive(true);
    }

    private void SpawnEnemyType1Horizontal()
    {
        GameObject ship = GameObject.Find("Koel");

        Vector2 rightPos = new Vector2(8.11f, ship.transform.position.y);
        Vector2 leftPos = new Vector2(-8.11f, ship.transform.position.y);

        if (ship.GetComponent<Koel>().shipPolarity == GameConstants.Polarity.light)
        {
            //light
            if (ship.transform.position.x <= -1.0f)
            {
                //right
                Instantiate(GameAssets.GetInstance().enemyType1RTL_light, rightPos, Quaternion.identity);
            }
            else
            {
                //left
                Instantiate(GameAssets.GetInstance().enemyType1LTR_light, leftPos, Quaternion.identity);
            }
        }
        else
        {
            //dark
            if (ship.transform.position.x <= -1.0f)
            {
                //right
                Instantiate(GameAssets.GetInstance().enemyType1RTL_dark, rightPos, Quaternion.identity);
            }
            else
            {
                //left
                Instantiate(GameAssets.GetInstance().enemyType1LTR_dark, leftPos, Quaternion.identity);
            }
        }
    }
}
