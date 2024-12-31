using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab; // Assign your block prefab in the Inspector
    public Transform player; // Reference to the player's Transform
    public float verticalSpacing = 3f; // Vertical distance between blocks
    public float horizontalRange = 2f; // Horizontal range for block placement
    private GameObject currentBlock; // The current block the player is interacting with
    private float lastBlockY = -4f; // Y position of the last spawned block

    void Start()
    {
        // Spawn the first block
        SpawnBlock();
    }

    void Update()
    {
        // Continuously check if the player is standing on the current block
        if (currentBlock != null && player.position.y > currentBlock.transform.position.y + 0.5f)
        {
            SpawnBlock();
        }
    }

    void SpawnBlock()
    {
        // Calculate the position for the new block
        float randomX = Random.Range(-horizontalRange, horizontalRange);
        float nextY = lastBlockY + verticalSpacing;

        Vector3 spawnPosition = new Vector3(randomX, nextY, 0);
        currentBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        // Update the Y position of the last spawned block
        lastBlockY = nextY;
    }
}