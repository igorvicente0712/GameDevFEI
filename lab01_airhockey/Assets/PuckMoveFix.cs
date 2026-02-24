using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuckAutoReset : MonoBehaviour
{
    public Vector2 centerPosition = Vector2.zero; // posição do centro do campo
    public float stuckSpeedThreshold = 0.05f;     // considera 0 se for menor que isso
    public float stuckTime = 3f;                  // segundos para considerar travado

    private Rigidbody2D rb;
    private float stuckTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 v = rb.linearVelocity;

        bool xZero = Mathf.Abs(v.x) < stuckSpeedThreshold;
        bool yZero = Mathf.Abs(v.y) < stuckSpeedThreshold;

        // Aqui você escolhe a condição:
        // - "xZero || yZero" = travou em qualquer eixo
        // - "xZero && yZero" = completamente parado
        bool isStuck = xZero || yZero;

        if (isStuck)
        {
            stuckTimer += Time.fixedDeltaTime;

            if (stuckTimer >= stuckTime)
            {
                // Teleporta pro centro e zera velocidade
                rb.position = centerPosition;
                rb.linearVelocity = Vector2.zero;
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }
    }
}
