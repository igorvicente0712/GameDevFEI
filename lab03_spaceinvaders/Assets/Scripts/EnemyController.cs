using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int scoreValue = 10; 
    private EnemyGroupController group;

    void Start()
    {
        group = GetComponentInParent<EnemyGroupController>();
    }

    public void OnDeath()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        if (group != null)
        {
            group.AtualizarVelocidade(this);
        }

        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Se encostar no jogador → derrota imediata
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            // Remove toda a vida de uma vez
            player.TakeDamage(player.maxLives);
        }
    }
}