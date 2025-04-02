using UnityEngine;

public class WallRun : MonoBehaviour {

    public bool OnWall = false;

    [Header("NewInput")]
    // New Input System
    private PlayerInputActions _inputActions;

    [Header("Movement")]
    [SerializeField] private Transform orientation;

    [Header("Wall Running")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;
    [SerializeField] private float yJumpForce;
    [SerializeField] public float fallMultiplier = 2.7f;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunfov;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }


    private bool wallLeft = false;
    private bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody rb;

    bool CanWallRun() {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void Start() {
        rb = GetComponent<Rigidbody>();

        _inputActions = new PlayerInputActions();
        if (_inputActions == null) {
            Debug.Log("Input Actions Is Null!");
        } else {
            _inputActions.Player.Enable();
        }
    }

    void CheckWall() {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    private void Update() {
        CheckWall();

        if(CanWallRun()) {
            if(wallLeft) {
                StartWallRun();
                OnWall = true;
            //    Debug.Log("wall running on left");
            }
            else if (wallRight) {
                StartWallRun();
                OnWall = true;
             //   Debug.Log("wall running on Right");

            } else {
                StopWallRun();
                OnWall = false;
            }
        }
        else {
            StopWallRun();
        }

    }

    void StartWallRun() {
        
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        if (wallLeft) { 
        tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force); }

        else if (wallRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);



        if (_inputActions.Player.Jump.triggered && PlayerSlide.Instance.justJumpedCoolDown == false) {
            if (wallLeft) {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, yJumpForce, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
                PlayerSlide.Instance.StartCoroutine(PlayerSlide.Instance.JumpedCoolDown());

            } else {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, yJumpForce, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
                PlayerSlide.Instance.StartCoroutine(PlayerSlide.Instance.JumpedCoolDown());

            }
        }

    }

    void StopWallRun() {
        rb.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);

        
    }

}
