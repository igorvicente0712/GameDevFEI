using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 8f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 direction = new Vector2(0.7f, 1f).normalized;
        rb.linearVelocity = direction * initialSpeed;
    }
}