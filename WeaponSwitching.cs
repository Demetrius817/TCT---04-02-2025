using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    [Header("NewInput")]
    // New Input System
    private PlayerInputActions _inputActions;


    public int selectedWeapon = 0;
    public GameObject GunScript;
    private void Start()
    {

        SelectWeapon();

        _inputActions = new PlayerInputActions();
        if (_inputActions == null) {
            Debug.Log("Input Actions Is Null!");
        } else {
            _inputActions.Player.Enable();
        }


    }


    private void Update()
    {
       

        int previousSelectedWeapon = selectedWeapon;
        if (!GameHandler.Instance.isGamePlaying()) return;
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (_inputActions.Player.SwitchPistol.triggered) {
                selectedWeapon = 0;
                //   Debug.Log("Gun1");
            }

            if (_inputActions.Player.SwitchPocketRocket.triggered && transform.childCount >= 2) {
                selectedWeapon = 1;
                if (selectedWeapon == 1) {
                    //    Debug.Log("Gun2");
                }
            }
            if (_inputActions.Player.SwitchGrappleGun.triggered && transform.childCount >= 3) {
                selectedWeapon = 2;
                if (selectedWeapon == 2) {
                    //     Debug.Log("Gun3");
                }
            }
            if (_inputActions.Player.SwitchPocketPortal.triggered && transform.childCount >= 4) {
                selectedWeapon = 3;
                if (selectedWeapon == 3) {
                    //  Debug.Log("Gun4");
                }
            }

            if (previousSelectedWeapon != selectedWeapon) {
                SelectWeapon();
            }
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform) {
            if (i == selectedWeapon) {
                weapon.gameObject.SetActive(true);
               
            } else {
                weapon.gameObject.SetActive(false);
               
              //  Debug.Log("switched");
            }
            i++;
        }
    }

}