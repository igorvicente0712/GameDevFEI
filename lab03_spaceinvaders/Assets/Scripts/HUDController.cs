using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public PlayerHealth player;

    void Start()
    {
        if (player == null)
        {
            player = Object.FindFirstObjectByType<PlayerHealth>();
        }
    }

    void Update()
    {
        AtualizarScore();
        AtualizarVidas();
    }

    void AtualizarScore()
    {
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.score;
        }
    }

    void AtualizarVidas()
    {
        if (player != null && livesText != null)
        {
            livesText.text = "Vidas: " + player.CurrentLives;
        }
    }
}