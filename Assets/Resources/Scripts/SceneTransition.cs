using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.Serialization;

public class SceneTransition : MonoBehaviour
{

    [FormerlySerializedAs("MenuScene")] [Header("Main Scene")]
    public string menuScene = "menu";
    [FormerlySerializedAs("LevelScene")] 
    public string levelScene = "level1";

    [FormerlySerializedAs("nameSceneAction")] [Header("Alt Scenes")]
    public string scene2;
    public string scene3;
    /*public string nameTowersScene;
    public string nameTrapScene;
    public string enemySceneName;
    public string uiSceneName;
    public string mapSceneName;*/

    public void loadMenuScene()
    {
        SceneManager.LoadSceneAsync(menuScene);
    }

    public void loadLevelScene()
    {
        SceneManager.LoadSceneAsync(levelScene);
    }

    public void loadScene2()
    {
        SceneManager.LoadScene(scene2);
    }

    public void loadScene3()
    {
        SceneManager.LoadScene(scene3);
    }
    /*public void loadTowerScene()
    {
        SceneManager.LoadScene(nameTowersScene);
        Debug.Log("Tower scene");
    }
    public void loadTrapScene()
    {
        SceneManager.LoadScene(nameTrapScene);
    }
    public void loadEnemyScene()
    {

        SceneManager.LoadScene(enemySceneName);
    }
    public void loadUiScene()
    {
        SceneManager.LoadScene(uiSceneName);
    }
    public void loadMapScene()
    {
        SceneManager.LoadScene(mapSceneName);
    }*/
}
