using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour {

    public static PlayerSlide Instance { get; private set; }


    [Header("NewInput")]
    // New Input System
    private PlayerInputActions _inputActions;
    private Vector2 _movementDirection;

    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("SlideJump")]
    [SerializeField] float jumpForce = 11;

    [Header("Cooldown")]
    [SerializeField] public float CooldownTime = 3f;
    [SerializeField] public float justJumpedCooldownTime = 1f;
    private bool justSlide = false;
    public bool SlideForcedStopped = false;

    [Header("Sliding")]
    [SerializeField] public float maxSlideTime;
    [SerializeField] public float slideForce;
    [SerializeField] private float slideTimer;
    [SerializeField] float slideSetSpeed = 11;
    [SerializeField] float scaleSpeed = 11;

    public float slideYScale;
    private float startYScale;
    public bool isSliding = false;

    private bool CanSlide = false;
public bool sliding = false;

    public bool justJumpedCoolDown = false;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float slidefov;
    [SerializeField] private float slidefovTime;

    public bool CanCam = false;

    [Header("Input")]
[SerializeField] public KeyCode slideKey = KeyCode.C;
//[SerializeField] KeyCode stopKey = KeyCode.Space;
private float horizontalInput;
private float verticalInput;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
    rb = GetComponent<Rigidbody>();
    pm = GetComponent<PlayerMovement>();


    startYScale = playerObj.localScale.y;

        _inputActions = new PlayerInputActions();
        if (_inputActions == null) {
            Debug.Log("Input Actions Is Null!");
        } else {
            _inputActions.Player.Enable();
        }

    }
    public void Update() {

        

        verticalInput = Mathf.Clamp(Input.GetAxis("Vertical"), 0f, Mathf.Infinity);

    if (_inputActions.Player.Slide.triggered && pm.isGrounded && CanSlide && !justSlide && (horizontalInput != 0 || verticalInput != 0)) {
            StartSlide();
            CanCam = true;
            SlideForcedStopped = false;
            isSliding = true;



        }
     
   
        Vector3 targetScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);

        if (_inputActions.Player.Jump.triggered && pm.isGrounded == true && sliding == false && justJumpedCoolDown == false) {
            pm.Jump();
            StartCoroutine(JumpedCoolDown());
        }
        else if (sliding) {
        if (_inputActions.Player.Jump.triggered && !pm.isGrounded && CanCam == true) {

                //We are stopping the slide, targetScale wont be changed anymore
                ForceStopSlide();
                Debug.Log("I am active");



            } else //We are still sliding so we set the Y target scale to "slideYScale"
            targetScale.y = slideYScale;
    }

    //Here is the sweet spot, I smootly move towards the target scale!
    playerObj.localScale = Vector3.MoveTowards(playerObj.localScale, targetScale, Time.deltaTime * scaleSpeed);

        if (CanCam == true) {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, slidefov, slidefovTime * Time.deltaTime );
        } else  {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, slidefovTime * Time.deltaTime);


        }
    }

    private void FixedUpdate() {
    if (sliding)
        SlidingMovement();
        
        if (rb.velocity.magnitude > slideSetSpeed) {
        CanSlide = true;
    } else //you didnt need another if condition, you are checking above
    {
        CanSlide = false;
    }
}
private void StartSlide() {

        sliding = true;
       
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    slideTimer = maxSlideTime;


        if (SlideForcedStopped == false)
        StartCoolDown();

    }
    public void StartCoolDown() {
    StartCoroutine(Cooldown());
}
private void SlidingMovement() {

        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        // sliding normal
        if (!pm.OnSlope() || rb.velocity.y > -0.1f) {
        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

        slideTimer -= Time.deltaTime;
    } else {
        // sliding down a slope
        rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
    }
    if (slideTimer <= 0)
        StopSlide();
}

    private void StopSlide() {

        sliding = false;
        CanCam = false;
        isSliding = false;
       

    }
    private void ForceStopSlide() {

        SlideForcedStopped = true;
        sliding = false;
        CanCam = false;
        isSliding = false;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);




    }


    private IEnumerator Cooldown() {
    justSlide = true;
    yield return new WaitForSeconds(CooldownTime);
    justSlide = false;
}

    public IEnumerator JumpedCoolDown() {
        justJumpedCoolDown = true;
        yield return new WaitForSeconds(justJumpedCooldownTime);
        justJumpedCoolDown = false;
    }

}

