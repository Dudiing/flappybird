// ObstacleSpawner.cs

using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float speed = 2f;
    public float destroyXPosition = -3f;
    private BirdController birdController;
    private GameObject currentObstacle;
    private bool canSpawnObstacles = false;

    void Start()
    {
        birdController = FindObjectOfType<BirdController>();
    }

    void Update()
    {
        if (birdController == null || !birdController.IsGameStarted)
        {
            return;
        }

        if (currentObstacle != null)
        {
            currentObstacle.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            if (currentObstacle.transform.position.x < destroyXPosition)
            {
                DestroyObstacle();
                SpawnObstacle();
            }
        }
    }

    void SpawnObstacle()
    {
        if (!canSpawnObstacles)
        {
            return;
        }

        float spawnYPosition = Random.Range(-2f, 3f);
        currentObstacle = Instantiate(obstaclePrefab, new Vector3(3f, spawnYPosition, 0), Quaternion.identity);
    }

    void DestroyObstacle()
    {
        Destroy(currentObstacle);
    }

    public void StartSpawningObstacles()
    {
        canSpawnObstacles = true;
        SpawnObstacle();
    }

    public void StopSpawningObstacles()
    {
        canSpawnObstacles = false;
    }
}
