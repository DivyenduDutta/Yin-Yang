using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Animator gameOverAnimator;

    public GameObject gameOverText;

    private void Awake()
    {
        gameOverAnimator = GetComponent<Animator>();
    }

    public void AnimateGameOverScreen()
    {
        gameOverAnimator.SetTrigger("GameOverAnimate");
        Invoke("BringInGameOverText", 1f);
    }

    private void BringInGameOverText()
    {
        gameOverText.SetActive(true);
    }
}
