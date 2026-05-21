using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string spawnID;

    private void Awake()
    {
        // Store the scene this spawn point belongs to for later reference
        gameObject.name = $"{spawnID}_SpawnPoint";
    }
}
