using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel playerModel;
    private GameObject plane;
    private TouchJoystick touchJoystick;
    private Rigidbody rigidBody;
    private Vector3 moveDirection;
    private float currentSpeed;
    private float minX, maxX, minZ, maxZ;
    private float playerHalfWidth, playerHalfLength;

    public void Initialize(GameObject plane)
    {
        playerModel = new PlayerModel();
        this.plane = plane;
        rigidBody = GetComponent<Rigidbody>();
        touchJoystick = FindObjectOfType<TouchJoystick>();

        // Calculate plane boundaries
        float planeHalfWidth = plane.transform.localScale.x * 5.0f;
        float planeHalfLength = plane.transform.localScale.z * 5.0f;

        minX = this.plane.transform.position.x - planeHalfWidth;
        maxX = this.plane.transform.position.x + planeHalfWidth;
        minZ = this.plane.transform.position.z - planeHalfLength;
        maxZ = this.plane.transform.position.z + planeHalfLength;

        // Calculate player size
        Collider playerCollider = GetComponent<Collider>();
        playerHalfWidth = playerCollider.bounds.size.x / 2;
        playerHalfLength = playerCollider.bounds.size.z / 2;
        
        minX += playerHalfWidth;
        maxX -= playerHalfWidth;
        minZ += playerHalfLength;
        maxZ -= playerHalfLength;
    }

    void Update()
    {
        Vector3 inputDirection = touchJoystick.GetInputDirection();
        float speedPercentage = inputDirection.magnitude;
        currentSpeed = playerModel.speed * speedPercentage;

        moveDirection = inputDirection.normalized;
        moveDirection.y = 0;
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection * currentSpeed, ForceMode.Acceleration);

        // Clamp the rigidbody's velocity to prevent slow movement beyond the edge
        Vector3 clampedVelocity = rigidBody.velocity;
        if (rigidBody.position.x <= minX && clampedVelocity.x < 0 || rigidBody.position.x >= maxX && clampedVelocity.x > 0)
        {
            clampedVelocity.x = 0;
        }
        if (rigidBody.position.z <= minZ && clampedVelocity.z < 0 || rigidBody.position.z >= maxZ && clampedVelocity.z > 0)
        {
            clampedVelocity.z = 0;
        }
        rigidBody.velocity = clampedVelocity;

        // Clamp the rigidbody's position to stay within the plane boundaries
        Vector3 newPosition = rigidBody.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        rigidBody.position = newPosition;
    }
}
