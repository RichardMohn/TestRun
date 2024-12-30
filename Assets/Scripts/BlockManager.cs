using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public float spawnInterval = 2f;
    public float blockSpeed = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBlock();
            timer = 0f;
        }
    }

    void SpawnBlock()
    {
        float randomX = Random.Range(-2f, 2f);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);
        GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        block.GetComponent<BlockMovement>().speed = blockSpeed;
    }
}
