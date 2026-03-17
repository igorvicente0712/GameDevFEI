using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public int CurrentLives => currentLives;

    void Start()
    {
        currentLives = maxLives;
        // Depois: atualizar UI
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        if (currentLives <= 0)
        {
            currentLives = 0;
            Die();
        }

        Debug.Log("Vidas restantes: " + currentLives);
        // Depois: atualizar UI
    }

        void Die()
    {
        Debug.Log("Jogador morreu!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }

        gameObject.SetActive(false);
    }
}