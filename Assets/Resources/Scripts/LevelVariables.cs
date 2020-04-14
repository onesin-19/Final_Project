using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class LevelVariables : MonoBehaviour
{
    #region Singleton
    public static LevelVariables instance;
    private void Awake() {

        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
    #endregion


    //[Header("Grid")]
    //[SerializeField] public Transform mapStartPointInMap;
    //[SerializeField] public Grid hiddenGridPrefab;
    //[SerializeField] public GameObject tileSidesPrefab;

    //[HideInInspector] public GridEntity mapGrid;
    //[HideInInspector] public Grid hiddenGrid;


    [Header("Player")]
    [SerializeField] public Player playerPrefab;
    [SerializeField] public AudioClip soudDead;
    [SerializeField] public Transform playerSpawnPosition;
    //[SerializeField] public Transform EndPosition;
    [SerializeField] public SceneFader sceneFader;

    /*[Header("Survivor")]
    [SerializeField] public GameObject Waypoint;*/

    [FormerlySerializedAs("enemyStart")]
    [Header("Enemies")]
    [SerializeField] public Spawner[] Spawn;
    [SerializeField] public GameObject enemyEnd;
    //[SerializeField] public GameObject enemyParentPoint;
    [SerializeField] public LevelSystem levelSystem;
    [SerializeField] public GameObject enemyPoint;
    //[SerializeField] public Countdown timerUI;

}
