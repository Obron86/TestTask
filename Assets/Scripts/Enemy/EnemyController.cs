using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyModel enemyModel;
    private PlayerController playerController;
    private Rigidbody enemyRigidbody;

    public void Initialize(EnemyModel enemyModel)
    {
        this.enemyModel = enemyModel;
    }
    
    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 directionToPlayer = (playerController.transform.position - transform.position).normalized;
        enemyRigidbody.velocity = directionToPlayer * enemyModel.speed;
    }
}