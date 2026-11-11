using UnityEngine;

public class cameraControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothSpeed = 10f; // Controls how fast the camera catches up
    
    private Vector3 distance;

    void Start()
    {
        if (player != null)
        {
            // Calculate the initial offset distance between camera and player
            distance = transform.position - player.transform.position;
        }
        else
        {
            Debug.LogWarning("Player GameObject is not assigned in cameraControl!");
        }
    }

    // Use LateUpdate for cameras to prevent stuttering and jitter
    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.transform.position + distance;

        // 2. Smoothly move toward the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // 3. Keep the camera looking at the player's upper body
        transform.LookAt(player.transform.position + Vector3.up * 1.5f);
    }
}