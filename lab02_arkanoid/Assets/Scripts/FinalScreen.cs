using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalScreen : MonoBehaviour
{
    [Header("Referências de UI")]
    public TextMeshProUGUI resultadoText;
    public TextMeshProUGUI scoreFinalText;

    [Header("Configuração")]
    [Tooltip("Cena para reiniciar o jogo (ex: Level1 ou MenuInicial)")]
    public string restartSceneName = "Level1";

    private void Start()
    {
        if (GameState.Instance != null && GameState.Instance.PlayerWon)
        {
            resultadoText.text = "Você ganhou!";
        }
        else
        {
            resultadoText.text = "Você perdeu!";
        }

        int finalScore = 0;
        if (ScoreManager.Instance != null)
        {
            finalScore = ScoreManager.Instance.CurrentScore;
        }

        scoreFinalText.text = "Score Final: " + finalScore;
    }

    public void ReiniciarJogo()
    {
        Debug.Log("Botão Reiniciar foi clicado!"); 
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        SceneManager.LoadScene(restartSceneName);
    }
}