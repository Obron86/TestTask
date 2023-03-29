using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    public void Initialize(GameObject plane, GameObject minePrefab, int mineCount)
    {
        (var minX, var maxX, var minZ, var maxZ) = PlaneUtilities.CalculatePlaneBoundaries(plane);

        for (int i = 0; i < mineCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX),
                plane.transform.position.y,
                Random.Range(minZ, maxZ)
            );

            Instantiate(minePrefab, spawnPosition, Quaternion.identity);
        }
    }
}