using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 8f;
    private bool isSprinting;

    [Header("MouseLook")]
    public Transform cam;
    public float sensitivity = 1f;

    [Header("Jumping")]
    public float jumpHeight = 2f;
    public float gravityscale = 5f;

    private CharacterController charactercontroller;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 playerInput;
    private Vector3 moveDirection;

    private float MouseX;
    private float MouseY;

    private Vector3 playerVelocity;

    public float inputSmoothTime = 0.25f;
    private Vector3 playerInputSmoothed;
    private Vector3 smoothPlayerInput;

    void Start()
    {
        charactercontroller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetInput();
        DoMouseLook();
        JumpPhysics();
        ManipulateController();
    }

    void DoMouseLook()
    {
        cam.localRotation = Quaternion.Euler(MouseY, 0, 0);
        transform.Rotate(0, MouseX, 0);
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        playerInput = new Vector3(horizontalInput, 0, verticalInput);

        MouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        MouseY -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        MouseY = Mathf.Clamp(MouseY, -90, 90);

        if (Input.GetButtonDown("Jump") && charactercontroller.isGrounded)
        {
            
            JumpPhysics();
        }

        smoothPlayerInput = Vector3.SmoothDamp
              (smoothPlayerInput, playerInput, ref playerInputSmoothed, inputSmoothTime);
    }

    void JumpPhysics()
    {
        if (Input.GetButtonDown("Jump")&&charactercontroller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
        else
        {
            playerVelocity.y += Physics.gravity.y * gravityscale * Time.deltaTime;
        }
    }

    void ManipulateController()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = isSprinting ? sprintSpeed : walkSpeed;
        moveDirection = transform.rotation * smoothPlayerInput;
        moveDirection *= speed;
        moveDirection.y = playerVelocity.y;
        charactercontroller.Move(moveDirection * Time.deltaTime);
    }
}
