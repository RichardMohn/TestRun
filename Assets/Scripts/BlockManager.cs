using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab; // Assign your block prefab in the Inspector
    public Transform player; // Reference to the player's Transform
    public float verticalSpacing = 3f; // Vertical distance between blocks
    public float horizontalRange = 2f; // Horizontal range for block placement
    private float lastBlockY = -4f; // Y position of the last spawned block

    void Start()
    {
        // Spawn initial blocks
        for (int i = 0; i < 5; i++) // Spawning initial blocks for gameplay
        {
            SpawnBlock();
        }
    }

    void Update()
    {
        // Check if a new block needs to be spawned
        if (player.position.y > lastBlockY - verticalSpacing * 3) // Keep a few blocks ahead
        {
            SpawnBlock();
        }
    }

    void SpawnBlock()
    {
        // Calculate random horizontal position and vertical placement
        float randomX = Random.Range(-horizontalRange, horizontalRange);
        float nextY = lastBlockY + verticalSpacing;

        // Spawn the block at the calculated position
        Vector3 spawnPosition = new Vector3(randomX, nextY, 0);
        Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        // Update the Y position of the last spawned block
        lastBlockY = nextY;
    }
}