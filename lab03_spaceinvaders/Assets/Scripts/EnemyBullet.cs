using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float minY = -6f;                // limite inferior da tela
    public GameObject explosionPrefab;      // PREFAB DE EXPLOSÃO

    void Update()
    {
        // mover para baixo
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // se passar do limite, explode
        if (transform.position.y < minY)
        {
            Explodir();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // acertou o player?
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(1);
            Explodir();
        }
    }

    void Explodir()
    {
        // INSTANTIA O PREFAB DE EXPLOSÃO, SE TIVER
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // depois destrói a bala
        Destroy(gameObject);
    }
}