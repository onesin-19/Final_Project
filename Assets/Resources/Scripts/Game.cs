using System.Collections.Generic;
using UnityEngine;

public class Game : Flow
{

    #region Singleton
    private static Game instance = null;

    public static Game Instance {
        get {
            return instance ?? (instance = new Game());
        }
    }
    #endregion

    //Managers
    LogicManager logicManager;
    TimeManager timeManager;

    PlayerManager playerManager;
    InputManager inputManager;

    //WaveManager waveManager;
    EnemyManager enemyManager;
    
    UIManager uiManager;

    private PauseManager pauseManager;

    private SurvivorManager survivorManager;
    //AmbianceManager ambianceManager;

    private GameVariables gameVariables;
    private LevelVariables mapVariables;
    private bool sceneEnded;
    private bool playerFixed = false;
    private ushort frameCount = 0;
    public bool IsThereSurvivor;

    override public void PreInitialize()
    {
        //Grab instances
        logicManager = LogicManager.Instance;
        timeManager = TimeManager.Instance;

        uiManager = UIManager.Instance;
        playerManager = PlayerManager.Instance;
        inputManager = InputManager.Instance;
        
        //waveManager = WaveManager.Instance;
        enemyManager = EnemyManager.Instance;
        pauseManager = PauseManager.Instance;
        survivorManager = SurvivorManager.Instance;

        //ambianceManager = AmbianceManager.Instance;

        //First Initialize
        logicManager.PreInitialize();
        timeManager.PreInitialize();

        uiManager.PreInitialize();
        pauseManager.PreInitialize();
        playerManager.PreInitialize();
        inputManager.PreInitialize();
        
        enemyManager.PreInitialize();
        if(IsThereSurvivor)
            survivorManager.PreInitialize();
        //waveManager.PreInitialize();
        
        //ambianceManager.PreInitialize();

        PreInitializeMap();
    }

    override public void Initialize()
    {
        logicManager.Initialize();
        timeManager.Initialize();

        uiManager.Initialize();
        pauseManager.Initialize();
        playerManager.Initialize();
        inputManager.Initialize();
        
        enemyManager.Initialize();
        if(IsThereSurvivor)
            survivorManager.Initialize();
        //waveManager.Initialize();
        
        //ambianceManager.Initialize();

        //Setup Variables
        gameVariables = GameVariables.Instance;
        mapVariables = LevelVariables.instance;
        sceneEnded = false;

        SpawnPlayer();
    }

    override public void Refresh()
    {
        if (!sceneEnded)
        {
            logicManager.Refresh();
            timeManager.Refresh();

            uiManager.Refresh();
            pauseManager.Refresh();
            playerManager.Refresh();
            inputManager.Refresh();

            //towerManager.Refresh();
            //trapManager.Refresh();

            enemyManager.Refresh();
            if(IsThereSurvivor)
                survivorManager.Refresh();
            //waveManager.Refresh();
            //ambianceManager.Refresh();
        }
    }

    override public void PhysicsRefresh()
    {
        if (!sceneEnded)
        {
            logicManager.PhysicsRefresh();
            timeManager.PhysicsRefresh();

            uiManager.PhysicsRefresh();
            pauseManager.PhysicsRefresh();
            playerManager.PhysicsRefresh();
            inputManager.PhysicsRefresh();

            //towerManager.PhysicsRefresh();
            //trapManager.PhysicsRefresh();
            
            enemyManager.PhysicsRefresh();
            if(IsThereSurvivor)
                survivorManager.PhysicsRefresh();
            //waveManager.PhysicsRefresh();

            //ambianceManager.PhysicsRefresh();
        }
    }

    override public void EndFlow()
    {
        sceneEnded = true;
        logicManager.EndFlow();
        timeManager.EndFlow();

        uiManager.EndFlow();
        pauseManager.EndFlow();
        playerManager.EndFlow();
        inputManager.EndFlow();

        enemyManager.EndFlow();
        if(IsThereSurvivor)
            survivorManager.EndFlow();
        //waveManager.EndFlow();

        //ambianceManager.EndFlow();


        DestroyMapVariables();
    }


    private void PreInitializeMap()
    {

    }

    private void InitializeMap()
    {
        //Spawn player at position
        SpawnPlayer();

    }

    private void DestroyMapVariables()
    {

    }

    private void SpawnPlayer()
    {
        //Spawn at position
        this.playerManager.player.transform.position = this.mapVariables.playerSpawnPosition.position;

    }
    
}
