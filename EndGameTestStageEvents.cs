using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndGameTestStageEvents : MonoBehaviour {
    [Header("BlackLines")]
    private float newValue = 4;
    private float oldValue = 21;
    private float duration = 4;
    private float someValue;

    [Header("References")]
    public GameObject Pistol;
    public GameObject DoorObject;
    public GameObject AmmoText;
    public GameObject BlackLine;
    public Image BlackLineImage;


   private void Start() {
        Pistol = GameObject.Find("PocketPistolAnimatedMesh2");
        DoorObject = GameObject.Find("DoorObjects");
        AmmoText = GameObject.Find("Ammo_Text");
        BlackLine = GameObject.Find("ExpandingBlackLine");

        BlackLine.gameObject.SetActive(false);
        BlackLineImage.rectTransform.sizeDelta = new Vector2(40, oldValue);

    }

    void Update() {
        if (EndGameTrigger.Instance.hitEndGame == true) {

            DoorObject.gameObject.SetActive(false);
            AmmoText.gameObject.SetActive(false);
            BlackLine.gameObject.SetActive(true);

            Pistol.GetComponent<Animator>().enabled = false;
            StartCoroutine(BlackLineClose());

            Debug.Log("Won");

        }

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
}
