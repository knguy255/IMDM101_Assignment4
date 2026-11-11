using UnityEngine;

public class bananaMover : MonoBehaviour
{
    private Vector3 startPosition; 

    [Header("Movement settings")]
    public float moveDistance = 0.5f;
    public float moveSpeed = 3f; 

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        float xOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPosition + new Vector3(xOffset, 0f, 0f);
    }
}
