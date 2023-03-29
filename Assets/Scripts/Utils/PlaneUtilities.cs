using UnityEngine;

public static class PlaneUtilities
{
    public static (float minX, float maxX, float minZ, float maxZ) CalculatePlaneBoundaries(GameObject plane)
    {
        var localScale = plane.transform.localScale;
        var planeHalfWidth = localScale.x * 5.0f;
        var planeHalfLength = localScale.z * 5.0f;

        var position = plane.transform.position;
        var minX = position.x - planeHalfWidth;
        var maxX = position.x + planeHalfWidth;
        var minZ = position.z - planeHalfLength;
        var maxZ = position.z + planeHalfLength;

        return (minX, maxX, minZ, maxZ);
    }
}