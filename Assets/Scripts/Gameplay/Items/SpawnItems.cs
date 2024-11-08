using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;
    private float spawnInterval;
    private float paddingPercentage = 0.1f;

    private float minX, maxX;

    private void Start()
    {
        ResetChild();
        CalculateSpawnBoundaries();
        SetSpawnIntervalBasedOnDifficulty();
    }

    void CalculateSpawnBoundaries()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        float padding = screenWidth * paddingPercentage;
        minX = -screenWidth / 2 + padding;
        maxX = screenWidth / 2 - padding;
    }

    void SetSpawnIntervalBasedOnDifficulty()
    {
        switch (GameSettings.Instance.SelectedDifficulty)
        {
            case GameSettings.Difficulty.Easy:
                spawnInterval = 1.0f;
                break;
            case GameSettings.Difficulty.Medium:
                spawnInterval = 0.75f;
                break;
            case GameSettings.Difficulty.Hard:
                spawnInterval = 0.5f;
                break;
        }
    }

    public void StartSpawning()
    {
        CancelInvoke(nameof(SpawnItem));
        InvokeRepeating(nameof(SpawnItem), 0.5f, spawnInterval);
        ResetChild();
    }

    public void ResetChild()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void SpawnItem()
    {
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject item = itemPrefabs[randomIndex];

        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0)).y, 0);

        GameObject spawnedItem = Instantiate(item, spawnPosition, Quaternion.identity);
        spawnedItem.transform.parent = transform;
    }
}
