using UnityEngine;
using TMPro;  // se você for usar TextMeshPro para UI

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int scoreBottom = 0; // pontos do jogador de baixo
    public int scoreTop = 0;    // pontos do jogador de cima

    public Transform puck;
    public Vector2 puckStartPosition = Vector2.zero;

    public TextMeshProUGUI bottomScoreText; // opcional, UI
    public TextMeshProUGUI topScoreText;    // opcional, UI

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ResetPuck();
        UpdateScoreUI();
    }

    public void GoalBottom() // PlayerTop marcou (puck entrou no gol de baixo)
    {
        scoreTop++;
        UpdateScoreUI();
        ResetPuck();
    }

    public void GoalTop() // PlayerBottom marcou (puck entrou no gol de cima)
    {
        scoreBottom++;
        UpdateScoreUI();
        ResetPuck();
    }

    private void ResetPuck()
    {
        if (puck == null)
            return;

        var rb = puck.GetComponent<Rigidbody2D>();
        puck.position = puckStartPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void UpdateScoreUI()
    {
        if (bottomScoreText != null)
            bottomScoreText.text = scoreBottom.ToString();

        if (topScoreText != null)
            topScoreText.text = scoreTop.ToString();
    }
}
