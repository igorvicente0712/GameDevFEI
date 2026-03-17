using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 5f;

    [Header("Limites da Tela")]
    public float minX;
    public float maxX;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 0.3f;

    private float nextFireTime = 0f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Calcula limites com base na câmera
        float halfPlayerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        float screenHalfWidthInWorld = mainCamera.orthographicSize * mainCamera.aspect;

        minX = -screenHalfWidthInWorld + halfPlayerWidth;
        maxX =  screenHalfWidthInWorld - halfPlayerWidth;
    }

    void Update()
    {
        Mover();
        Atirar();
    }

    void Mover()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // A/D ou setas

        Vector3 pos = transform.position;
        pos.x += inputX * speed * Time.deltaTime;

        // Limita na tela
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }

        void Atirar()
    {
        // Espaço ou botão de fogo padrão ("Fire1")
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // Instancia o tiro na posição do shootPoint (ou da nave)
            Vector3 spawnPos = shootPoint != null ? shootPoint.position : transform.position;
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        }
    }
}