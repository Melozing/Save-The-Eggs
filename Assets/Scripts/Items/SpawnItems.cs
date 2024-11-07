using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs; // Array of different item prefabs to spawn
    private float spawnInterval = 0.62f; // Time interval between each spawn
    private float paddingPercentage = 0.1f; // Percentage padding from the edges of the screen

    private float minX, maxX; // Variables to hold the minimum and maximum x coordinates for spawning

    void Start()
    {
        ResetChild();
        // Calculate the screen boundaries in world coordinates, considering padding
        CalculateSpawnBoundaries();
    }

    void CalculateSpawnBoundaries()
    {
        // Get the screen width in world units based on the camera's aspect ratio
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2; // Width of the camera view in world units

        // Calculate padding based on the screen width
        float padding = screenWidth * paddingPercentage;

        // Adjust minX and maxX considering the calculated padding
        minX = -screenWidth / 2 + padding; // Adjust minimum x coordinate
        maxX = screenWidth / 2 - padding;  // Adjust maximum x coordinate
    }

    public void StartSpawning()
    {
        // Cancel any previous repeating invocations to prevent stacking
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
        // Randomly select an item from the itemPrefabs array
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject item = itemPrefabs[randomIndex];

        // Calculate a random spawn position within the minX and maxX bounds
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0)).y, 0);

        // Instantiate the item at the calculated spawn position
        GameObject spawnedItem = Instantiate(item, spawnPosition, Quaternion.identity);

        // Set the spawned item's parent to the object that spawns it
        spawnedItem.transform.parent = transform; // Set the parent to this SpawnItems object
    }
}
