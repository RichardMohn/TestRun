using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab; // Reference to the block prefab
    public float spawnInterval = 2f; // Time interval between spawning blocks
    public float blockSpeed = 2f; // Speed of the block's movement
    public float minHeight = 1f; // Minimum height difference between blocks
    public float maxHeight = 3f; // Maximum height difference between blocks

    private float timer; // Timer to track when to spawn blocks

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBlock();
            timer = 0f; // Reset the timer
        }
    }

    void SpawnBlock()
    {
        // Generate random X position for the block
        float randomX = Random.Range(-2f, 2f);

        // Generate a random height offset for vertical spacing
        float randomY = Random.Range(minHeight, maxHeight);

        // Calculate the spawn position
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y + randomY, 0);

        // Instantiate the block at the calculated position
        GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        // Assign speed to the block's movement
        if (block.GetComponent<BlockMovement>() != null)
        {
            block.GetComponent<BlockMovement>().speed = blockSpeed;
        }
    }
}