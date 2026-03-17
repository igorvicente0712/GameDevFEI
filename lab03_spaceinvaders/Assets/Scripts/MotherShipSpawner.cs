using UnityEngine;

public class MotherShipSpawner : MonoBehaviour
{
    public GameObject motherShipPrefab;
    public float spawnIntervalMin = 15f;
    public float spawnIntervalMax = 30f;
    public float yPosition = 4.0f;      // altura em que ela passa
    public float speed = 3f;

    private Camera mainCamera;
    private float nextSpawnTime;

    void Start()
    {
        mainCamera = Camera.main;
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMotherShip();
            ScheduleNextSpawn();
        }
    }

    void ScheduleNextSpawn()
    {
        float intervalo = Random.Range(spawnIntervalMin, spawnIntervalMax);
        nextSpawnTime = Time.time + intervalo;
    }

    void SpawnMotherShip()
    {
        if (motherShipPrefab == null || mainCamera == null)
            return;

        // decide aleatoriamente se vem da esquerda ou da direita
        bool fromLeft = Random.value < 0.5f;

        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        float startX = fromLeft ? -camWidth / 2f - 1f : camWidth / 2f + 1f;
        float endX   = fromLeft ?  camWidth / 2f + 1f : -camWidth / 2f - 1f;

        Vector3 startPos = new Vector3(startX, yPosition, 0f);

        GameObject ship = Instantiate(motherShipPrefab, startPos, Quaternion.identity);

        MotherShipMover mover = ship.AddComponent<MotherShipMover>();
        mover.targetX = endX;
        mover.speed   = speed;
    }
}