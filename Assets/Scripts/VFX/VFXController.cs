using UnityEngine;
using System;

public class VFXController : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    
    public void SpawnExplosion(Vector3 position)
    {
        Instantiate(explosionPrefab, position, Quaternion.identity);
    }
}