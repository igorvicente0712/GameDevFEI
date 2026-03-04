using UnityEngine;

public class Block : MonoBehaviour
{
    public int scoreValue = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ScoreManager.Instance.AddScore(scoreValue);

            Destroy(gameObject);
        }
    }
}