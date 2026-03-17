using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxHeight = 1f;
    public GameObject explosionPrefab;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > maxHeight)
        {
            Explodir();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1) Tenta ver se é MotherShip
        MotherShipHealth mother = other.GetComponent<MotherShipHealth>();
        if (mother != null)
        {
            mother.TakeHit();
            Explodir();
            return;
        }

        // 2) Caso contrário, inimigo normal
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.OnDeath();
            Explodir();
        }
    }

    void Explodir()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}