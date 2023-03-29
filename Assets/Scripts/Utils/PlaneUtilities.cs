using UnityEngine;

public static class PlaneUtilities
{
    public static (float minX, float maxX, float minZ, float maxZ) CalculatePlaneBoundaries(GameObject plane)
    {
        var localScale = plane.transform.localScale;
        float planeHalfWidth = localScale.x * 5.0f;
        float planeHalfLength = localScale.z * 5.0f;

        var position = plane.transform.position;
        float minX = position.x - planeHalfWidth;
        float maxX = position.x + planeHalfWidth;
        float minZ = position.z - planeHalfLength;
        float maxZ = position.z + planeHalfLength;

        return (minX, maxX, minZ, maxZ);
    }
}