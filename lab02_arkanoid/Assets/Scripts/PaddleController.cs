using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float limitX = 9.9f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // A/D ou setas

        Vector2 velocity = new Vector2(input * speed, 0f);
        rb.linearVelocity = velocity;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        transform.position = pos;
    }
}