using UnityEngine;
using UnityEngine.InputSystem;

public class playerControl : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int bananaCount = 0;

    public float speed = 10f;
    public Transform movementCamera; 

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        
        // Automatically find the Main Camera if slot is empty
        if (movementCamera == null && Camera.main != null)
        {
            movementCamera = Camera.main.transform;
        }
    }

    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate() 
    {
        if (rb == null) return;

        // 1. Get camera directions, flattening the Y axis so we stay grounded
        Vector3 forward = movementCamera != null ? movementCamera.forward : Vector3.forward;
        Vector3 right = movementCamera != null ? movementCamera.right : Vector3.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // 2. Calculate movement relative to where the camera is pointing
        Vector3 moveDirection = (right * movementX + forward * movementY);

        // 3. Apply velocity to Rigidbody
        Vector3 targetVelocity = moveDirection * speed;
        targetVelocity.y = rb.linearVelocity.y; // Keep vertical gravity (use rb.velocity.y if on older Unity versions)
        rb.linearVelocity = targetVelocity;

        // 4. Smoothly rotate the monkey to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Collectible")) {
            other.gameObject.SetActive(false); 
            bananaCount++;
            Debug.Log("Bananas collected: " + bananaCount + " / 39");
        }
    }
}
