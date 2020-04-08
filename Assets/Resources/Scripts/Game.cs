﻿using System.Collections.Generic;
using UnityEngine;

public class Game : Flow
{

    #region Singleton
    static private Game instance = null;

    static public Game Instance {
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

    //TowerManager towerManager;
    //TrapManager trapManager;

    //WaveManager waveManager;
    EnemyManager enemyManager;

    //PodManager podManager;
    //ArrowManager arrowManager;
    //rojectileManager projectileManager;

    UIManager uiManager;
    //AmbianceManager ambianceManager;

    private GameVariables gameVariables;
    private LevelVariables mapVariables;
    private bool sceneEnded;
    private bool playerFixed = false;
    private ushort frameCount = 0;

    override public void PreInitialize()
    {
        //Grab instances
        logicManager = LogicManager.Instance;
        timeManager = TimeManager.Instance;

        uiManager = UIManager.Instance;
        playerManager = PlayerManager.Instance;
        inputManager = InputManager.Instance;

        //towerManager = TowerManager.Instance;
        //trapManager = TrapManager.Instance;

        //waveManager = WaveManager.Instance;
        enemyManager = EnemyManager.Instance;

        //podManager = PodManager.Instance;
        //arrowManager = ArrowManager.Instance;
        //projectileManager = ProjectileManager.Instance;

        //ambianceManager = AmbianceManager.Instance;

        //First Initialize
        logicManager.PreInitialize();
        timeManager.PreInitialize();

        uiManager.PreInitialize();
        playerManager.PreInitialize();
        inputManager.PreInitialize();

        //towerManager.PreInitialize();
        //trapManager.PreInitialize();

        enemyManager.PreInitialize();
        //waveManager.PreInitialize();

        //podManager.PreInitialize();
        //arrowManager.PreInitialize();
        //projectileManager.PreInitialize();

        //ambianceManager.PreInitialize();

        PreInitializeMap();
    }

    override public void Initialize()
    {
        logicManager.Initialize();
        timeManager.Initialize();

        uiManager.Initialize();
        playerManager.Initialize();
        inputManager.Initialize();

        //towerManager.Initialize();
        //trapManager.Initialize();
        
        enemyManager.Initialize();
        //waveManager.Initialize();

        //podManager.Initialize();
        //arrowManager.Initialize();
        //projectileManager.Initialize();

        //ambianceManager.Initialize();

        //Setup Variables
        gameVariables = GameVariables.instance;
        mapVariables = LevelVariables.instance;
        sceneEnded = false;

        InitializeMap();
    }

    override public void Refresh()
    {
        if (!sceneEnded)
        {
            logicManager.Refresh();
            timeManager.Refresh();

            uiManager.Refresh();
            playerManager.Refresh();
            inputManager.Refresh();

            //towerManager.Refresh();
            //trapManager.Refresh();

            enemyManager.Refresh();

            //waveManager.Refresh();

            //podManager.Refresh();
            //arrowManager.Refresh();
            //projectileManager.Refresh();

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
            playerManager.PhysicsRefresh();
            inputManager.PhysicsRefresh();

            //towerManager.PhysicsRefresh();
            //trapManager.PhysicsRefresh();
            
            enemyManager.PhysicsRefresh();

            //waveManager.PhysicsRefresh();

            //podManager.PhysicsRefresh();
            //arrowManager.PhysicsRefresh();
            //projectileManager.PhysicsRefresh();

            //ambianceManager.PhysicsRefresh();
        }
    }

    override public void EndFlow()
    {
        sceneEnded = true;
        logicManager.EndFlow();
        timeManager.EndFlow();

        uiManager.EndFlow();
        playerManager.EndFlow();
        inputManager.EndFlow();

        //towerManager.EndFlow();
        //trapManager.EndFlow();
        
        enemyManager.EndFlow();

        //waveManager.EndFlow();

        //podManager.EndFlow();
        //arrowManager.EndFlow();
        //projectileManager.EndFlow();

        //ambianceManager.EndFlow();


        DestroyMapVariables();
    }


    private void PreInitializeMap()
    {

    }

    private void InitializeMap()
    {
        //Create grid to place items
        //InitGrid();

        //Create enemy start/end point
        //StartEndPath(this.gameVariables.pathTilesCoords[0], this.gameVariables.pathTilesCoords[this.gameVariables.pathTilesCoords.Count - 1]);

        //Create enemy path on grid
        //PlacePointInMap();

        //Spawn items on map
        //SpawnItemsOnGrid();

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

   

   /* private void InitGrid()
    {
        //Init grids holder
        this.gameVariables.gridsHolder = new GameObject("GridsStuff");

        //Init hidden grid
        this.mapVariables.hiddenGrid = GameObject.Instantiate<Grid>(this.mapVariables.hiddenGridPrefab, this.gameVariables.gridsHolder.transform);

        //Init map grid entity
        this.mapVariables.mapGrid = new GridEntity("MapMap", this.mapVariables.hiddenGrid, this.mapVariables.mapStartPointInMap, this.gameVariables.mapRows, this.gameVariables.mapColumns, this.gameVariables.inactiveTilesCoords, this.gameVariables.pathTilesCoords, this.mapVariables.tileSidesPrefab);

    }*/

    /*private void SpawnItemsOnGrid()
    {
        //Get info package
        //Dictionary<Vector2, TowerType> towersInfo = MapInfoPck.Instance.TileTowerInfos;
        //Dictionary<Vector2, TrapType> trapsInfo = MapInfoPck.Instance.TileTrapInfos;

        //generates towers without vr
        //MapInfoPck.Instance.TestPopulate();

        //Towers
        foreach (KeyValuePair<Vector2, TowerType> info in towersInfo)
        {
            //Get tile Coords
            Vector2 tileCoords = this.mapVariables.mapGrid.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.mapVariables.mapGrid.GetTileCenterFixed(tileCoords);

            //Create obj
            Tower tower = towerManager.CreateTower(info.Value, tileCenter);
        }

        //Traps
        foreach (KeyValuePair<Vector2, TrapType> info in trapsInfo)
        {
            //Get tile Coords
            Vector2 tileCoords = this.mapVariables.mapGrid.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.mapVariables.mapGrid.GetTileCenterFixed(tileCoords);

            //Create obj
            trapManager.CreateTrap(info.Value, tileCenter);
        }
    }*/

    /*private void StartEndPath(Vector2 startPath, Vector2 endPath)
    {
        this.mapVariables.enemyStart.transform.position = this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(startPath));
        this.mapVariables.enemyEnd.transform.position = this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(endPath));
        this.mapVariables.enemyPoint.transform.position = this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(startPath));
    }*/

    /*private void PlacePointInMap()
    {
        GameObject enemyPoint = this.mapVariables.enemyParentPoint;
        foreach (Vector2 vec in GameVariables.instance.pathTilesCoords)
        {
            GameObject ob = GameObject.Instantiate(this.mapVariables.enemyPoint,
                this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(vec)),
                Quaternion.identity,
                enemyPoint.transform);
        }
        //EnemyManager.Instance.SetPoints(enemyPoint.transform);
        //GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player/TeleportPoint"), enemyPoint.transform.GetChild(enemyPoint.transform.childCount - 1).transform.position, Quaternion.identity);
    }*/
}
