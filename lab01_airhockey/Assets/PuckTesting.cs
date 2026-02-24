using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuckTestKick : MonoBehaviour
{
    public Vector2 initialVelocity = new Vector2(5f, 3f);

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Para versões novas da Unity (que descontinuaram velocity no Inspector),
        // usa-se linearVelocity
        rb.linearVelocity = initialVelocity;
    }
}
