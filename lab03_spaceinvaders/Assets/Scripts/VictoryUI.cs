using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        if (GameManager.Instance != null && finalScoreText != null)
        {
            finalScoreText.text = "Score: " + GameManager.Instance.score;
        }
    }

    public void OnClickPlayAgain()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetScore();
        }

        SceneManager.LoadScene("Level");
    }
}