using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab; // Assign the Block Prefab in the Inspector
    public Transform player; // Reference to the player's Transform
    public float blockSpawnInterval = 2f; // Distance between blocks
    public float horizontalRange = 2f; // Horizontal range for block placement
    public float verticalSpacing = 3f; // Vertical spacing between blocks

    private float lastBlockY = -4; // Tracks the Y position of the last spawned block

    void Start()
    {
        // Spawn the first block above the ground
        SpawnBlock();
    }

    void Update()
    {
        // Continuously check if a new block needs to be spawned
        if (player.position.y > lastBlockY - blockSpawnInterval)
        {
            SpawnBlock();
        }
    }

    void SpawnBlock()
    {
        // Randomize the horizontal position within range
        float randomX = Random.Range(-horizontalRange, horizontalRange);
        float nextY = lastBlockY + verticalSpacing;

        // Spawn the block at the calculated position
        Vector3 spawnPosition = new Vector3(randomX, nextY, 0);
        Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        // Update the Y position of the last block
        lastBlockY = nextY;
    }
}