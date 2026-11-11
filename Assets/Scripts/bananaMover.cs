using UnityEngine;

public class bananaMover : MonoBehaviour
{
    private Vector3 startPosition; 
    private float randomDistance;
    private float randomSpeed;

    [Header("Movement settings")]
    public float moveDistance = 4f;
    public float moveSpeed = 5f; 

    void Start() {
        startPosition = transform.position;
        randomDistance = Random.Range(1f, moveDistance);
        randomSpeed = Random.Range(3f, moveSpeed);
    }

    void Update() {
        float zOffset = Mathf.Sin(Time.time * randomSpeed) * randomDistance;
        transform.position = startPosition + new Vector3(0f, 0f, zOffset);
    }
}
