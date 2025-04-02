using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStateHandler_TestStage : MonoBehaviour {

    [Header("References")]
    public GameObject Pistol;
    [SerializeField] private GameObject StartTEXT;
    public GameObject AmmoText;
    public GameObject AmmoText2;
    public GameObject Environment;
    public GameObject tempWalls;
    public GameObject Door;


    void Start() {
        StartTEXT = GameObject.Find("PressToStart_Text");
        AmmoText = GameObject.Find("Ammo_Text");
        AmmoText2 = GameObject.Find("Ammo_Text_Con");
        Pistol = GameObject.Find("PocketPistolAnimated");

        Environment = GameObject.Find("----- ENVIRONMENT -----");
        tempWalls = GameObject.Find("TutorialTempWallSystem");
        Door = GameObject.Find("Door");

    }

    void Update() {
        if (GameHandler.Instance.isGameWaitingToStart()) {
            StartTEXT.gameObject.SetActive(true);
            AmmoText.gameObject.SetActive(false);
            AmmoText2.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(false);

            Environment.gameObject.SetActive(false);
            tempWalls.gameObject.SetActive(false);
            Door.gameObject.SetActive(false);



        }
        if (!GameHandler.Instance.isGameWaitingToStart()) {
            StartTEXT.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(true);

            Environment.gameObject.SetActive(true);
            tempWalls.gameObject.SetActive(true);
            Door.gameObject.SetActive(true);

        }
        if (GameHandler.Instance.isGamePlaying()) {
            AmmoText.gameObject.SetActive(true);
            AmmoText2.gameObject.SetActive(true);

        }
    }
    }
