using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {



    float playerHeight = 2f;
    [SerializeField] Transform orientation;
    private PlayerSlide ps;

    [Header("NewInput")]
    // New Input System
    private PlayerInputActions _inputActions;
    private Vector2 _movementDirection;



    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;
    float horizontalMovement;
    float verticalMovement;


    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float RunStepsVolume = 0.2f;
   private bool JustWalked = false;


    [Header("Jumping")]
    public float jumpForce = 5f;
    [SerializeField] public float fallMultiplier = 2.8f;
    [SerializeField] public float lowJumpMultiplier = 2f;


    [Header("Keybinds")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;


    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded;
    public bool justJumped = false;



    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;


    public bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f)) {
            if (slopeHit.normal != Vector3.up) {
                return true;
            } else {
                return false;
            }

        }
        return false;
    }
    public Vector3 GetSlopeMoveDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<PlayerSlide>();

        rb.freezeRotation = true;

        _inputActions = new PlayerInputActions();
        if (_inputActions == null) {
            Debug.Log("Input Actions Is Null!");
        } else {
            _inputActions.Player.Enable();
        }

    }

    private void Update() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //GameHandler Controlling player
        if (GameHandler.Instance.isGameWaitingToStart()) {
            rb.isKinematic = true;
            GetComponent<PlayerLook>().enabled = false;
        }

        if (GameHandler.Instance.isCountingDownToStart()) {
            rb.isKinematic = false;
            GetComponent<PlayerLook>().enabled = false;
        }
        if (GameHandler.Instance.isGamePlaying()) {
            rb.isKinematic = false;
            GetComponent<PlayerLook>().enabled = true;
        }
        if (GameHandler.Instance.isGameWon()) {
            rb.isKinematic = true;
            GetComponent<PlayerLook>().enabled = false;
        }

        if (!isGrounded) {
            justJumped = true;
        }
        if (justJumped == true && isGrounded) {
            StartCoroutine(JustLandedAfterJumping());
        }

    }


    void MyInput() {
        // This is player movement 
        if (!GameHandler.Instance.isGamePlaying()) return;
        _movementDirection = _inputActions.Player.Move.ReadValue<Vector2>();
        horizontalMovement = _movementDirection.x;
        verticalMovement = _movementDirection.y;

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

    }



    public void Jump() {
        {
            if (!GameHandler.Instance.isGamePlaying()) return;

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
         //   Debug.Log("I am active TOOOO");

        }

    }

    void ControlSpeed() {
        if (Input.GetKey(sprintKey) && isGrounded) {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        } else {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);

        }
    }

    void ControlDrag() {
        if (isGrounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = airDrag;
        }
    }
    private void FixedUpdate() {
        MovePlayer();
    }

    private IEnumerator JustLandedAfterJumping() {
        justJumped = true;
        yield return new WaitForSeconds(0.4f);// Play with this value a bit.
        justJumped = false;
    }


    void MovePlayer() {
        if (isGrounded && !OnSlope()) {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            if (JustWalked == false && rb.velocity.magnitude > 9 && justJumped == false) {
                StartCoroutine(PlayFootstepSound());
            }
        } else if (isGrounded && OnSlope()) {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        } else if (!isGrounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);

        }
    }

    private IEnumerator PlayFootstepSound() {
            SoundManager.PlaySound(SoundType.Running, RunStepsVolume);
            JustWalked = true;
            yield return new WaitForSeconds(0.3f);// Play with this value a bit.
            JustWalked = false;
    }

    

}

