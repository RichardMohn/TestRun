using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab; // Reference to the block prefab
    public Transform player; // Reference to the player
    public ScoreManager scoreManager; // Reference to the ScoreManager

    public float spawnHeight = 5f; // Distance above the player where new blocks spawn
    public float minBlockWidth = 1f; // Minimum width of a block
    public float maxBlockWidth = 3f; // Maximum width of a block

    private Vector3 lastBlockPosition; // Tracks the last block's position

    void Start()
    {
        // Initialize the first block
        lastBlockPosition = new Vector3(0, player.position.y - 2f, 0);
        SpawnBlock();
    }

    void Update()
    {
        // Continuously check if new blocks need to spawn
        if (player.position.y + spawnHeight > lastBlockPosition.y)
        {
            SpawnBlock();
        }
    }

    void SpawnBlock()
    {
        // Randomize block width and position
        float blockWidth = Random.Range(minBlockWidth, maxBlockWidth);
        Vector3 blockPosition = new Vector3(Random.Range(-2f, 2f), lastBlockPosition.y + Random.Range(2f, 3f), 0);

        // Spawn the block
        GameObject newBlock = Instantiate(blockPrefab, blockPosition, Quaternion.identity);
        newBlock.transform.localScale = new Vector3(blockWidth, newBlock.transform.localScale.y, newBlock.transform.localScale.z);

        // Track the new block's position
        lastBlockPosition = blockPosition;

        // Attach scoring logic to the block
        BlockLogic blockLogic = newBlock.AddComponent<BlockLogic>();
        blockLogic.scoreManager = scoreManager;
        blockLogic.player = player;
    }
}