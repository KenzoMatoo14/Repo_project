using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 7f;
    [Range(0.1f, 0.5f)] public float groundCheckDistance = 0.2f;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float staminaRecoveryRate = 1f;
    private float currentStamina;
    private bool isExhausted;

    [Header("Camera")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;

    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded;
    private Vector3 moveDirection;   

    [Header("Animation")]
    public Animator animator;
    private bool isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        currentStamina = maxStamina;
    }

    void Update()
    {
        RotateView();
        HandleStamina();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (animator != null)
        {
            bool isMoving = moveDirection.magnitude > 0.1f;
            if (isMoving != isWalking)
            {
                isWalking = isMoving;
                animator.SetBool("IsWalking", isWalking);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                animator.SetTrigger("Jump");
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized; 
        float speed = GetCurrentSpeed();

        Vector3 velocity = moveDirection * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    float GetCurrentSpeed()
    {
        bool wantsToRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isMoving = moveDirection.magnitude > 0.1f; 
        
        if (wantsToRun && !isExhausted && isMoving)
        {
            currentStamina -= Time.deltaTime;
            return runSpeed;
        }
        return walkSpeed;
    }

    void HandleStamina()
    {
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        isExhausted = currentStamina <= 0;
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void RotateView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);
        cameraTransform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), $"Stamina: {currentStamina.ToString("F1")}/{maxStamina}");
    }
}