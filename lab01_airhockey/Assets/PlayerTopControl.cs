using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerTopController : MonoBehaviour
{
    public Transform puck;

    // Limites do CAMPO (bordas reais, sem compensar raio)
    public float fieldMinX;  // parede esquerda
    public float fieldMaxX;  // parede direita
    public float fieldMinY;  // linha do meio
    public float fieldMaxY;  // parede de cima

    public float margin = 0.0f;   // folguinha opcional
    public float moveSpeed = 25f; // velocidade da IA

    private Rigidbody2D rb;
    private float radius;
    private Vector2 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var col = GetComponent<CircleCollider2D>();
        radius = col.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);

        // começa mirando na posição atual
        targetPosition = rb.position;

        // configs físicas básicas
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (puck == null)
            return;

        Vector3 puckPos = puck.position;

        float targetX = puckPos.x;
        float targetY = puckPos.y;

        // limites ajustados pelo raio + margem
        float minX = fieldMinX + radius + margin;
        float maxX = fieldMaxX - radius - margin;
        float minY = fieldMinY + radius + margin;
        float maxY = fieldMaxY - radius - margin;

        float clampedX = Mathf.Clamp(targetX, minX, maxX);
        float clampedY = Mathf.Clamp(targetY, minY, maxY);

        targetPosition = new Vector2(clampedX, clampedY);
    }

    private void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            targetPosition,
            moveSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);
    }
}
