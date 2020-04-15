using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;

public class Main : MonoBehaviour
{

    //static public Main Instance { get; private set; }
    #region Singleton
    private static Main instance = null;

    static public Main Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Main>();//AddComponent<Main>();
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
    
    double time;
    double currentTime;

    private VideoPlayer video;

    private bool canRefresh = false;
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
        
        if (SceneManager.GetActiveScene().name == "level2")
            game.IsThereSurvivor = true;
        if (SceneManager.GetActiveScene().name == "menu")
        {
            currentFlow = menuFlow;
            isInMenuScene = true;
        }
        else if (SceneManager.GetActiveScene().name == "level1"||
                 SceneManager.GetActiveScene().name == "level2"||
                 SceneManager.GetActiveScene().name == "level3")
        {
            currentFlow = game;
            //ambiance.playMapMusic();
            isInMenuScene = false;
            
        }
        else
        {
            Debug.LogWarning("Not supposed to happen. Wrong scene name. Loading Room Flow.");
            currentFlow = game;
        }

        if (SceneManager.GetActiveScene().name == "level1")
        {
            video = gameObject.GetComponent<VideoPlayer>();
            time = video.clip.length;
        }
        

    }

    private void Start()
    {
        //currentFlow.Initialize();
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "level1")
        {
            currentTime = video.time;
            if (currentTime < time)
            {

                if (PlayerManager.Instance.player)
                {
                    PlayerManager.Instance.player.SetActive(false);
                    PlayerManager.Instance.player.GetComponent<FirstPersonController>().UnlockMouse();
                    UIManager.Instance.HideUI();
                }
               
            }
            else
            {
                currentFlow.Refresh();
                canRefresh = true;
                PlayerManager.Instance.player.SetActive(true);
                UIManager.Instance.ShowUI();
            }
        }
        else
        {
            currentFlow.Refresh();
            canRefresh = true;
            PlayerManager.Instance.player.SetActive(true);
            UIManager.Instance.ShowUI();
        }
        

    }

    private void FixedUpdate()
    {
        if (canRefresh)
            currentFlow.PhysicsRefresh();
        
    }

    private void EndFlow()
    {
        currentFlow.EndFlow();
    }

    public void ChangeCurrentFlow(bool isWin)
    {
        EndFlow();
        //sceneTransition = gameObject.GetComponent<SceneTransition>();
        if (!isWin)
        {
            sceneTransition.loadMenuScene();
            Debug.Log("changement de scene");
            //SceneManager.LoadScene("menu");
            //isInMenuScene = true;
        }
        else
        {
            if (currentSceneName=="level1")
            {
                SceneManager.LoadScene("level2");
            }
            else if (currentSceneName=="level2")
            {
                SceneManager.LoadScene("menu");
            }
            //sceneTransition.loadLevelScene();
            //isInMenuScene = false;
        }

        //instance = null;
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
        if (/*currentSceneName != "menu"lastSceneName*/!isInMenuScene&&!PlayerStats.IsPlayerDead&&PlayerStats.HasSaveSurvivor)
        {
            //grabbableObjects.Clear();
            //interactObjects.Clear();
            currentFlow.PreInitialize();
            currentFlow.Initialize();
            lastSceneName = currentSceneName;
            isInMenuScene = true;
        }
    }

    public void SkipTrailer()
    {
        video.time = time;
        UIManager.Instance.HideCanvas();
    }

    public void OpenMenu()
    {
        PauseManager.Instance.MainMenu();
    }
    
    public void ContinuePlay()
    {
        PauseManager.Instance.Toggle();
    }
}
