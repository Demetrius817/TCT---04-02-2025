using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonColorHandler : MonoBehaviour, ISelectHandler, IDeselectHandler {
    [Header("BoxColorReferences")]
    [SerializeField] private GameObject NumBoxOne;
    [SerializeField] private GameObject NumBoxTwo;

    [SerializeField] private GameObject PointLightOne;
    [SerializeField] private GameObject PointLightTwo;
    [SerializeField] private GameObject PointLightThree;


    public bool IsSelected { get; private set; } = false;

    public void OnSelect(BaseEventData eventData) {
      //  Debug.Log(this.gameObject.name + " was selected");
        NumBoxOne.SetActive(true);
        NumBoxTwo.SetActive(true);

        PointLightOne.SetActive(true);
        PointLightTwo.SetActive(true);
        PointLightThree.SetActive(true);
    }
    public void OnDeselect(BaseEventData data) {
       // Debug.Log("Deselected");
        NumBoxOne.SetActive(false);
        NumBoxTwo.SetActive(false);

        PointLightOne.SetActive(false);
        PointLightTwo.SetActive(false);
        PointLightThree.SetActive(false);

    }
}

