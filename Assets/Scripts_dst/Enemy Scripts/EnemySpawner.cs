using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>(); // List of enemy prefabs
    public GameObject player; // Reference to the player object
    public float spawnRate = 2f; // Time interval between spawns
    public float spawnRadius = 5f; // Distance from the player to spawn enemies

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not assigned to EnemySpawner!");
            return;
        }
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Generate a random position around the player within the spawn radius
            Vector2 spawnPos = (Vector2)player.transform.position + Random.insideUnitCircle.normalized * spawnRadius;

            // Randomly select an enemy prefab from the list
            if (Enemies.Count > 0)
            {
                int enemyIndex = Random.Range(0, Enemies.Count);
                Instantiate(Enemies[enemyIndex], spawnPos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No enemies in the list to spawn.");
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
