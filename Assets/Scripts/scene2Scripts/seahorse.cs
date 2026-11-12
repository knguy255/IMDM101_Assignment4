using UnityEngine;

public class seahorse : MonoBehaviour
{
    private Vector3 startPosition;

    [Header("Movement settings")]
    public float moveDistance = .5f;
    public float moveSpeed = 1f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
    }
}
