using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FitCameraToBackground : MonoBehaviour
{
    // Arraste aqui o objeto do Background (que tem o SpriteRenderer)
    public SpriteRenderer backgroundSpriteRenderer;

    // Se quiser um pouquinho de folga nas bordas
    public float padding = 0f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true; // Garante que está em modo ortográfico
    }

    private void Start()
    {
        if (backgroundSpriteRenderer == null)
        {
            Debug.LogWarning("FitCameraToBackground: backgroundSpriteRenderer não está atribuído.");
            return;
        }

        // Bounds do sprite do background em unidades de mundo
        Bounds bgBounds = backgroundSpriteRenderer.bounds;

        // Centraliza a câmera no centro do background
        Vector3 camPos = bgBounds.center;
        camPos.z = transform.position.z; // mantém o Z original da câmera (geralmente -10)
        transform.position = camPos;

        // Altura e largura do background em unidades de mundo
        float bgHeight = bgBounds.size.y;
        float bgWidth = bgBounds.size.x;

        // Ajuste com padding opcional
        bgHeight += padding * 2f;
        bgWidth += padding * 2f;

        // Calcula o orthographicSize necessário para caber a altura inteira
        float sizeByHeight = bgHeight / 2f;

        // Calcula o size necessário para caber a largura inteira, respeitando o aspect da tela
        float worldScreenHeight = bgHeight;
        float worldScreenWidth = worldScreenHeight * cam.aspect;

        // Se a largura do background é maior que a largura visível, precisamos aumentar o size
        if (bgWidth > worldScreenWidth)
        {
            // Tamanho necessário para caber a largura
            float sizeByWidth = (bgWidth / cam.aspect) / 2f;
            cam.orthographicSize = sizeByWidth;
        }
        else
        {
            // Só com a altura já cabe tudo
            cam.orthographicSize = sizeByHeight;
        }
    }
}
