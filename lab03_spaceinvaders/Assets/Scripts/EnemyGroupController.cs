using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    [Header("Dificuldade Dinâmica")]
    public float baseMoveSpeed = 2f;
    public float maxMoveSpeed = 8f;

    private int totalInimigos;

    [Header("Formação")]
    public GameObject[] enemyPrefabs;
    public int rows = 4;
    public int columns = 8;
    public float horizontalSpacing = 1.2f;
    public float verticalSpacing = 1f;

    [Header("Movimento")]
    public float moveSpeed = 2f;
    public float moveDownAmount = 0.5f;

    [Header("Ataque")]
    public GameObject enemyBulletPrefab;
    public float minShootInterval = 1.0f;
    public float maxShootInterval = 3.0f;

    private float nextShootTime;

    private bool movingRight = true;
    private float maxX;
    private float minX;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        CalcularLimites();
        CriarFormacao();
        ContarInimigosIniciais();
        moveSpeed = baseMoveSpeed;
        AtualizarVelocidade();
        DefinirProximoTiro();
    }

    void CalcularLimites()
    {
        float screenHalfWidthInWorld = mainCamera.orthographicSize * mainCamera.aspect;
        maxX = screenHalfWidthInWorld - 0.5f; // margem
        minX = -screenHalfWidthInWorld + 0.5f;
    }

    void CriarFormacao()
    {
        // Gera uma grade de inimigos como filhos do EnemyGroup
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 pos = new Vector3(
                    col * horizontalSpacing,
                    -row * verticalSpacing,
                    0f
                );

                Vector3 spawnPos = transform.position + pos;
                GameObject enemyPrefabEscolhido = EscolherEnemyPrefab(row, col);
                GameObject enemy = Instantiate(enemyPrefabEscolhido, spawnPos, Quaternion.identity, transform);
            }
        }

        // Centralizar a formação em relação ao EnemyGroup
        float totalWidth = (columns - 1) * horizontalSpacing;
        float totalHeight = (rows - 1) * verticalSpacing;
        Vector3 offset = new Vector3(totalWidth / 2f, -totalHeight / 2f, 0f);

        foreach (Transform child in transform)
        {
            child.position -= offset;
        }
    }

        GameObject EscolherEnemyPrefab(int row, int col)
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Nenhum enemyPrefab configurado no EnemyGroupController!");
            return null;
        }

        // Exemplo 1: aleatório
        // int index = Random.Range(0, enemyPrefabs.Length);

        // Exemplo 2 (opcional): por linha (linha de cima um tipo, linha de baixo outro, etc.)
        int index = row % enemyPrefabs.Length;

        return enemyPrefabs[index];
    }

    void MoverFormacao()
    {
        Vector3 pos = transform.position;

        float direction = movingRight ? 1f : -1f;
        pos.x += direction * moveSpeed * Time.deltaTime;
        transform.position = pos;

        // Verificar se algum inimigo bateu na borda
        foreach (Transform enemy in transform)
        {
            if (enemy == null) continue;

            if (movingRight && enemy.position.x >= maxX)
            {
                DescerEMudarDirecao();
                break;
            }
            else if (!movingRight && enemy.position.x <= minX)
            {
                DescerEMudarDirecao();
                break;
            }
        }
    }

    void DescerEMudarDirecao()
    {
        movingRight = !movingRight;
        Vector3 pos = transform.position;
        pos.y -= moveDownAmount;
        transform.position = pos;
    }

    void ContarInimigosIniciais()
    {
        totalInimigos = 0;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<EnemyController>() != null)
            {
                totalInimigos++;
            }
        }

        Debug.Log("Total de inimigos iniciais: " + totalInimigos);
    }

    public void AtualizarVelocidade(EnemyController morto = null)
    {
        int inimigosRestantes = 0;

        foreach (Transform child in transform)
        {
            if (child != null)
            {
                var enemy = child.GetComponent<EnemyController>();

                // ignora o que acabou de morrer
                if (enemy != null && enemy != morto)
                {
                    inimigosRestantes++;
                }
            }
        }

        Debug.Log("Inimigos restantes (reais): " + inimigosRestantes);

        if (inimigosRestantes == 0)
        {
            Debug.Log("Chamando Victory pelo EnemyGroupController");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.Victory();
            }
            return;
        }

        // cálculo da velocidade dinâmica
        if (totalInimigos > 0)
        {
            float destruidoPercent = 1f - (inimigosRestantes / (float)totalInimigos);
            moveSpeed = Mathf.Lerp(baseMoveSpeed, maxMoveSpeed, destruidoPercent);
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
    }

    void DefinirProximoTiro()
    {
        nextShootTime = Time.time + Random.Range(minShootInterval, maxShootInterval);
    }

    void Update()
    {
        MoverFormacao();
        GerenciarTiroInimigos();
    }

    void GerenciarTiroInimigos()
    {
        if (Time.time < nextShootTime)
            return;

        // Escolhe um inimigo para atirar
        Transform atirador = EscolherInimigoParaAtirar();
        if (atirador != null && enemyBulletPrefab != null)
        {
            Instantiate(enemyBulletPrefab, atirador.position, Quaternion.identity);
        }

        DefinirProximoTiro();
    }

    Transform EscolherInimigoParaAtirar()
    {
        Transform[] vivos = ObterInimigosVivos();
        if (vivos.Length == 0)
            return null;

        // Agrupa por "coluna" com base na posição X arredondada
        var colunas = new System.Collections.Generic.Dictionary<int, Transform>();

        foreach (Transform enemy in vivos)
        {
            // Converte a posição X em uma "coluna" inteira
            int coluna = Mathf.RoundToInt(enemy.position.x * 10f); 
            // (multiplico por 10 para evitar problemas de float, mas poderia ser só RoundToInt)

            // Queremos o inimigo MAIS PERTO DO PLAYER em cada coluna.
            // Dependendo de como seu eixo Y está, isso é o menor Y (mais embaixo).
            if (!colunas.ContainsKey(coluna))
            {
                colunas[coluna] = enemy;
            }
            else
            {
                // Se este inimigo está mais perto do player (Y menor), substitui
                if (enemy.position.y < colunas[coluna].position.y)
                {
                    colunas[coluna] = enemy;
                }
            }
        }

        // Agora temos um inimigo "da frente" por coluna.
        var candidatos = new System.Collections.Generic.List<Transform>(colunas.Values);

        if (candidatos.Count == 0)
            return null;

        // Escolhe um aleatório entre os da frente
        int index = Random.Range(0, candidatos.Count);
        return candidatos[index];
    }

    Transform[] ObterInimigosVivos()
    {
        System.Collections.Generic.List<Transform> lista = new System.Collections.Generic.List<Transform>();

        foreach (Transform enemy in transform)
        {
            if (enemy != null)
            {
                lista.Add(enemy);
            }
        }

        return lista.ToArray();
    }
}