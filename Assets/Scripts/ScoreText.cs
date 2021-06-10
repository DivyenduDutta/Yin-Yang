using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        scoreText.text = ScoreHandler.GetInstance().score.ToString();

        if (LevelHandler.GetInstance().isGameOver)
        {
            this.gameObject.SetActive(false);
        }
    }
}
