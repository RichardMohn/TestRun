void SpawnBlock()
{
    float randomX = Random.Range(-2f, 2f); // Random horizontal position
    float randomY = Random.Range(1f, 3f); // Random vertical spacing between blocks
    Vector3 spawnPosition = new Vector3(randomX, transform.position.y + randomY, 0);
    GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
    block.GetComponent<BlockMovement>().speed = blockSpeed;
}