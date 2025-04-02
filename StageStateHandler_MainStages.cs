using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageStateHandler_MainStages : MonoBehaviour {
    public GameObject MonsterTEXT;
    public GameObject MonsterNumTEXT;
    public GameObject StartTEXT;
    public GameObject AmmoText;
    public GameObject AmmoText2;

    public GameObject Environment;
    public GameObject Door;
    public GameObject Pistol;

    public void Start() {
        MonsterTEXT = GameObject.Find("MonsterDistance_Text");
        MonsterNumTEXT = GameObject.Find("MonsterDistanceNumber_Text");
        StartTEXT = GameObject.Find("PressToStart_Text");
        AmmoText = GameObject.Find("Ammo_Text");
        AmmoText2 = GameObject.Find("Ammo_Text_Con");

        Environment = GameObject.Find("----- ENVIRONMENT -----");
        Door = GameObject.Find("Door");
        Pistol = GameObject.Find("PocketPistolAnimatedMesh2");

    }

    private void Update() {


        if (GameHandler.Instance.isGameWaitingToStart()) {

            MonsterTEXT.gameObject.SetActive(false);
            MonsterNumTEXT.gameObject.SetActive(false);
            AmmoText.gameObject.SetActive(false);
            AmmoText2.gameObject.SetActive(false);
            StartTEXT.gameObject.SetActive(true);

            Environment.gameObject.SetActive(false);
            Door.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(false);

        }
        if (!GameHandler.Instance.isGameWaitingToStart()) {
            MonsterTEXT.gameObject.SetActive(true);
            MonsterNumTEXT.gameObject.SetActive(true);
            Environment.gameObject.SetActive(true);
            Door.gameObject.SetActive(true);
            Pistol.gameObject.SetActive(true);

            StartTextBreak();
        }
        if (GameHandler.Instance.isGamePlaying()) {
            AmmoText.gameObject.SetActive(true);
            AmmoText2.gameObject.SetActive(true);

        }

    }

        void StartTextBreak() {

        StartTEXT.gameObject.SetActive(false);
    }
}