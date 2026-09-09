using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the spawn manager in the new scene
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager != null && spawnManager.spawnPoint != null)
        {
            transform.position = spawnManager.spawnPoint.position;
            transform.rotation = spawnManager.spawnPoint.rotation; // Optional for setting rotation
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

