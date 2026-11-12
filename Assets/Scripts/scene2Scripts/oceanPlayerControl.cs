using UnityEngine;
using UnityEngine.InputSystem;

public class oceanPlayerControl : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float rotationSpeed = 720f;

    // monkey speech bubble
    public GameObject speechBubble;
    public float speechBubbleDelay = 3f;



    public Transform movementCamera;
    public InputActionReference moveAction; // expects Vector2
    public InputActionReference jumpAction; // expects Button

    private CharacterController controller;
    private float verticalSpeed;
    private float startingXRotation;
    private float startingZRotation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        startingXRotation = transform.eulerAngles.x;
        startingZRotation = transform.eulerAngles.z;
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void FixedUpdate()
    {   // How to use manul input (GetKeyDown Approach)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q down");
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Q up");
        }
    }


    void Start()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
            Invoke(nameof(HideSpeechBubble),
                speechBubbleDelay);
        }
    }

    
    void HideSpeechBubble()
    {
        speechBubble.SetActive(false);
    }


    void Update()
    {
  

        // 1. Read movement input (x: left/right, y: forward/backward).
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // 2. Use the camera's forward and right directions to move along the ground.
        Vector3 forward = movementCamera.forward;
        Vector3 right = movementCamera.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = right * input.x + forward * input.y;
        // Prevent faster movement along diagonals.
        move = Vector3.ClampMagnitude(move, 1f);

        // 3. Rotate the character toward the movement direction.
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            Vector3 targetEulerAngles = targetRotation.eulerAngles;

            targetRotation = Quaternion.Euler(
                startingXRotation,
                targetEulerAngles.y,
                startingZRotation);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 4. Underwater movement has no ground gravity or jumping.
        verticalSpeed = 0f;
        Vector3 velocity = move * playerSpeed;
        velocity.y = verticalSpeed;
        controller.Move(velocity * Time.deltaTime);
    }

    private int bananaCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            bananaCount++;
            Debug.Log("Bananas collected: " + bananaCount + " / 5");
        }
    }

}