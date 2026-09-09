using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint; // Assign the spawn point in the inspector

    private void Awake()
    {
        // Ensure there's only one SpawnManager in the scene
        SpawnManager[] managers = FindObjectsOfType<SpawnManager>();
        if (managers.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
