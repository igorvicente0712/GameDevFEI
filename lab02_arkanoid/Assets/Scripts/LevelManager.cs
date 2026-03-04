using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Nome da próxima cena (ex: Level2 ou Final)")]
    public string nextSceneName;

    private void Update()
    {
        var blocks = FindObjectsByType<Block>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        if (blocks.Length == 0)
        {
            if (nextSceneName == "Final" && GameState.Instance != null)
            {
                GameState.Instance.SetWin();
            }

            SceneManager.LoadScene(nextSceneName);
        }
    }
}