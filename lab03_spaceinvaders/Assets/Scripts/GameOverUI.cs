using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        if (GameManager.Instance != null && finalScoreText != null)
        {
            finalScoreText.text = "Score: " + GameManager.Instance.score;
        }
    }

    public void OnClickRetry()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetScore();
        }

        SceneManager.LoadScene("Level");
    }
}