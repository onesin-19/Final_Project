﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    //static public Main Instance { get; private set; }
    #region Singleton
    private static Main instance = null;

    static public Main Instance
    {
        /*get
        {
            return instance ?? (instance = new BulletManager());
        }*/
        get
        {
            if (instance == null){
                GameObject go = new GameObject();
                instance = go.AddComponent<Main>();
            }
            return instance;
        }
    }
   
    #endregion
    private Game game;
    private MenuFlow menuFlow;
    private Flow currentFlow;


    /*[Header("Internal Settings")]
    public GameObject RoomSetupPrefab;*/
    //[HideInInspector]
    //public GameObject VRPlayerCharacter;

    //public Dictionary<GameObject, GrabbableObject> grabbableObjects;
    //public Dictionary<GameObject, InteractObject> interactObjects;

    //public Player playerPrefab;
    public SceneTransition sceneTransition;

    public bool isInMenuScene { get; private set; }

    private string currentSceneName;
    private string lastSceneName;

    //public AmbianceManager ambiance;

    private void Awake()
    {

        /*#region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion*/

        //Vars
        isInMenuScene = true;

        //Initialize
        game = Game.Instance;
        menuFlow = MenuFlow.Instance;
        //ambiance = AmbianceManager.Instance;
        //grabbableObjects = new Dictionary<GameObject, GrabbableObject>();
        //interactObjects = new Dictionary<GameObject, InteractObject>();
        //ambiance = AmbianceManager.Instance;
        //ambiance.Initialize();
        
        //Get/Set
        sceneTransition = gameObject.GetComponent<SceneTransition>();

        //Scene Loading Delegate
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name == "menu")
        {
            currentFlow = game;
            isInMenuScene = false;
        }
        else if (SceneManager.GetActiveScene().name == "level1")
        {
            currentFlow = game;
            //ambiance.playMapMusic();
            isInMenuScene = false;
        }
        else
        {
            Debug.LogWarning("Not supposed to happen. Wrong scene name. Loading Room Flow.");
            currentFlow = menuFlow;
        }
    }

    private void Start()
    {
        //currentFlow.Initialize();
    }

    private void Update()
    {
        currentFlow.Refresh();
    }

    private void FixedUpdate()
    {
        currentFlow.PhysicsRefresh();
    }

    private void EndFlow()
    {
        currentFlow.EndFlow();
    }

    public void ChangeCurrentFlow()
    {
        EndFlow();
        //sceneTransition = gameObject.GetComponent<SceneTransition>();
        if (!isInMenuScene)
        {
            //sceneTransition.loadMenuScene();
            SceneManager.LoadScene("menu");
            isInMenuScene = true;
        }
        else
        {
            sceneTransition.loadLevelScene();
            isInMenuScene = false;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        currentFlow = game;
        /*if (!isInMenuScene)
        {
            currentFlow = game;
        }
        else
        {
            currentFlow = menuFlow;
        }*/
        //Make sure this is called only once.
        if (currentSceneName != lastSceneName&&!PlayerStats.IsPlayerDead)
        {
            //grabbableObjects.Clear();
            //interactObjects.Clear();
            currentFlow.PreInitialize();
            currentFlow.Initialize();
            lastSceneName = currentSceneName;
        }
    }
}
