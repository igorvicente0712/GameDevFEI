using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public string gameOverSceneName = "Final";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (GameState.Instance != null)
            {
                GameState.Instance.SetLose();
            }

            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}