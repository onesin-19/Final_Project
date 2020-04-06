using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : Flow {
    #region Singleton
    static private LogicManager instance = null;

    static public LogicManager Instance
    {
        get
        {
            return instance ?? (instance = new LogicManager());
        }
    }

    #endregion

    public bool IsGameOver { get; set; }

    override public void PreInitialize() { }

    override public void Initialize() { }

    override public void Refresh()
    {
        if (PlayerStats.IsPlayerDead)
            LevelLost();

    }

    override public void PhysicsRefresh() { }

    override public void EndFlow()
    {
        instance = null;
    }

    public void LevelWon()
    {
        UIManager.Instance.ShowVictory();
        //Change scene
        TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            Main.Instance.ChangeCurrentFlow();
        }, 4f));
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        Main.Instance.ChangeCurrentFlow();
    }
    public void LevelLost()
    {
        Main.Instance.StartCoroutine(GameOver());
        //UIManager.Instance.ShowBloodScreen();
        /*TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            Main.Instance.ChangeCurrentFlow();
        }, 3f));*/
    }

    public void GameFinish()
    {
        //------------------TODO----------------- Implement
    }
}
