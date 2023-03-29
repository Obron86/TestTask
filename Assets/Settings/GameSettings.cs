using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public float playerSpeed = 10f;
        public float enemySpeed = 3f;
        public int mineCount = 5;
        public int maxEnemies = 5;
        public float enemySpawnRate = 3f;
    }
}