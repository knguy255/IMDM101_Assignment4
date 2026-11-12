using UnityEngine;

public class crab : MonoBehaviour {

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float xOffset = Mathf.Sin(Time.time * 1f) * 5f;
        transform.position = startPosition + new Vector3(xOffset, 0f, 0f);
    }
}
