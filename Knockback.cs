using UnityEngine;
using System.Collections;

// Applies an explosion force to rigidbodies
public class Knockback : MonoBehaviour
{
   [SerializeField] public float power = 100.0F;
    [SerializeField] public float powerTwo = 100.0F;

    [Header("NewInput")]
    // New Input System
    private PlayerInputActions _inputActions;


    [Header("References")]
    [SerializeField] private Rigidbody RB;
    public GameObject player;
    public Transform cameraTransform;

    [Header("Use")]
    [SerializeField] private KeyCode Knock;
    [SerializeField] private KeyCode Knock2;
    float startTime = 0f;
    float holdTime = 5.0f;

    private void Start()
    {
        RB = player.GetComponent<Rigidbody>();

        _inputActions = new PlayerInputActions();
        if (_inputActions == null) {
            Debug.Log("Input Actions Is Null!");
        } else {
            _inputActions.Player.Enable();
        }

    }

    private void Update()
    {

        {
            if (_inputActions.Player.Shoot.triggered) {

                RB.AddForce(-cameraTransform.forward * power, ForceMode.Impulse);
                PRocketWeaponAnimation.Instance.Wanimator.SetTrigger("TrigFire");

            }
            if (_inputActions.Player.MorePowerRocket.triggered) {
                RB.AddForce(-cameraTransform.forward * powerTwo, ForceMode.Impulse);
            }
           
        }

    }
}
