using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class EndGameStageThreeEvents : MonoBehaviour
{

    [Header("BlackLines")]
    private float newValue = 4;
    private float oldValue = 21;
    private float duration = 4;
    private float someValue;

    [Header("FOVChange")]
    private float Camduration = 4;
    private float CamoldValue = 90;
    private float CamnewValue;
    private bool WinCoolDown = false;
    private float CooldownTime = 4;

    [Header("NewInput")]
    private PlayerInputActions _inputActions;


    [Header("References")]
    public GameObject DoorObject;
    public GameObject Monster;
    public GameObject MonsterTEXT;
    public GameObject AmmoText;
    public GameObject Pistol;
    public GameObject BlackLine;
    public GameObject CamObject;
    public Image BlackLineImage;
    public Camera cam;

    void Start()
    {
            _inputActions = new PlayerInputActions();
            if (_inputActions == null) {
                Debug.Log("Input Actions Is Null!");
            } else {
                _inputActions.Player.Enable();
            }


            DoorObject = GameObject.Find("DoorObjects");
            Monster = GameObject.Find("MonsterHandler");
            MonsterTEXT = GameObject.Find("MonsterTEXT");
            AmmoText = GameObject.Find("Ammo_Text");
            Pistol = GameObject.Find("PocketPistolAnimatedMesh2");
            BlackLine = GameObject.Find("ExpandingBlackLine");
            CamObject = GameObject.Find("Main Camera");

            BlackLine.gameObject.SetActive(false);
            BlackLineImage.rectTransform.sizeDelta = new Vector2(40, oldValue);

    }

    void Update()
    {
        if (EndGameTrigger.Instance.hitEndGame == true && WinCoolDown == true && _inputActions.Player.Jump.triggered) {
            StartCoroutine(FOVChange());
            BlackLine.gameObject.SetActive(false);


        }

        if (EndGameTrigger.Instance.hitEndGame == true) {
            DoorObject.gameObject.SetActive(false);
            Monster.gameObject.SetActive(false);
            MonsterTEXT.gameObject.SetActive(false);
            AmmoText.gameObject.SetActive(false);
            BlackLine.gameObject.SetActive(true);
            StartCoroutine(BlackLineClose());

            FullScreenEffects.Instance._fullscreeneffect.SetActive(false);
            FullScreenEffects.Instance._fullscreeneffecttwo.SetActive(false);

            Pistol.GetComponent<Animator>().enabled = false;
            StartWinContinueCoolDown();


            Debug.Log("Won");

        }

    }
    public void StartWinContinueCoolDown() {
        StartCoroutine(Cooldown());
    }


    private IEnumerator BlackLineClose() {
        BlackLineImage.rectTransform.sizeDelta = new Vector2(40, oldValue);

        float elapsedTime = 0;
        while (elapsedTime < duration) {

            elapsedTime += Time.deltaTime;

            float newValue = Mathf.Lerp(oldValue, 4f, (elapsedTime / duration));

            BlackLineImage.rectTransform.sizeDelta = new Vector2(40, newValue);

            yield return null;
        }


    }
    private IEnumerator FOVChange() {

        cam.fieldOfView = CamoldValue;

        float elapsedTime = 0;
        while (elapsedTime < Camduration) {

            elapsedTime += Time.deltaTime;

            float CamnewValue = Mathf.Lerp(CamoldValue, 179f, (elapsedTime / Camduration));

            cam.fieldOfView = CamnewValue;

            yield return null;
        }


    }
    private IEnumerator Cooldown() {
        yield return new WaitForSeconds(CooldownTime);
        WinCoolDown = true;

    }

}
