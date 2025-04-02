using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    [Header("BoxColorRefernces")]
    [SerializeField] private GameObject NumBoxOne;
    [SerializeField] private GameObject NumBoxTwo;

    [Header("PointLightReferences")]
    [SerializeField] private GameObject PointLightOne;
    [SerializeField] private GameObject PointLightTwo;
    [SerializeField] private GameObject PointLightThree;

    [Header("CanvasReferences")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _openMenuCanvas;
    [SerializeField] private GameObject _openLevelsCanvas;
    [SerializeField] private GameObject _LevelOneCanvas;

    [Header("ButtonReferences")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button openMenuButton;
    [SerializeField] private Button openLevelOneButton;
    [SerializeField] private Button levelOneFreeRunStageButton;
    [SerializeField] private Button levelOneStageOneButton;

    [Header("FirstSelectedReferences")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _levelsMenuFirst;
    [SerializeField] private GameObject _levelOneFirst;


    private void Awake() {
        _mainMenuCanvas.SetActive(false);
        _openLevelsCanvas.SetActive(false);
        _openMenuCanvas.SetActive(true);
        _LevelOneCanvas.SetActive(false);

        //Button Actions
        openMenuButton.onClick.AddListener(() => {
            OpenMenu();
        });
        playButton.onClick.AddListener(() => {
            OpenLevels();
        });
        openLevelOneButton.onClick.AddListener(() => {
            OpenLevelOne();
        });
        levelOneFreeRunStageButton.onClick.AddListener(() => {
            Loader.LoadLevelOneFreeRunStage(Loader.Scene.ALevelOneFreeRunStageScene);
        });
        levelOneStageOneButton.onClick.AddListener(() => {
            Loader.LoadLevelOneStageOne(Loader.Scene.BLevelOneStageOneScene);
        });
    }

    private void OpenMenu() {
        _mainMenuCanvas.SetActive(true);
        _openMenuCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
    private void OpenLevels() {
        _mainMenuCanvas.SetActive(false);
        _openLevelsCanvas.SetActive(true);
        _openMenuCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_levelsMenuFirst);
    }
    private void OpenLevelOne() {
        _openLevelsCanvas.SetActive(false);
        _LevelOneCanvas.SetActive(true);

        NumBoxOne.SetActive(false);
        NumBoxTwo.SetActive(false);

        PointLightOne.SetActive(false);
        PointLightTwo.SetActive(false);
        PointLightThree.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_levelOneFirst);

    }
}
