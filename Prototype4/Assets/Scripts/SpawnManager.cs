using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializedFeild] private GameObject enemyPrefab;
    [SerializedFeild] private GameObject powerupPrefab;
    [SerializedFeild] private float spawnRange = 9.0f;
    [SerializedFeild] private int waveNumber = 1;
    [SerializedFeild] private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }

    private void SpawnEnemyWave()
    {
        for (var i = 0; i < waveNumber; i++)
        {
            Vector3 spawnPos = GenerateSpawnPosition();
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        //get the amount of enemies in play
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave();
        }
    }
}
