using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;

    private Rigidbody rigidBody;
    private Vector3 moveDirection;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * playerModel.speed * Time.fixedDeltaTime);
    }
}