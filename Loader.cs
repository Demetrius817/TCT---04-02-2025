using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene {
            MenuScene,
            FreeRunStageLoadingScene,
            StageOneLoadingScene,
            ALevelOneFreeRunStageScene,
            BLevelOneStageOneScene
    }
    private static Scene targetScene;

    public static void LoadLevelOneFreeRunStage(Scene targetScene) {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.FreeRunStageLoadingScene.ToString());

    }
    public static void LoadLevelOneStageOne(Scene targetScene) {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.StageOneLoadingScene.ToString());

    }

    public static void LoaderCallBack() {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
