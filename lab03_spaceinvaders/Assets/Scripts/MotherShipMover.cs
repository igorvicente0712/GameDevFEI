using UnityEngine;

public class MotherShipMover : MonoBehaviour
{
    public float targetX;
    public float speed = 3f;

    void Update()
    {
        Vector3 pos = transform.position;

        float direction = targetX > pos.x ? 1f : -1f;
        pos.x += direction * speed * Time.deltaTime;
        transform.position = pos;

        // Se passar bem além do alvo, destrói
        if (direction > 0f && pos.x > targetX + 0.5f ||
            direction < 0f && pos.x < targetX - 0.5f)
        {
            Destroy(gameObject);
        }
    }
}