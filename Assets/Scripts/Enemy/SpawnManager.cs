using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints; // Puntos donde aparecerán los enemigos
    public GameObject enemyPrefab;  // Prefab del enemigo

    public void SpawnWave(int waveSize)
    {
        Debug.Log($"[SpawnManager] Generando una oleada de {waveSize} enemigos.");

        for (int i = 0; i < waveSize; i++)
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("[SpawnManager] No hay puntos de spawn asignados.");
                return;
            }

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Debug.Log($"[SpawnManager] Punto de spawn seleccionado: {spawnPoint.position}");
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

}
