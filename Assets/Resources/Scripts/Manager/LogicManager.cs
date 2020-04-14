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

    public bool IsLevelWin { get; set; }

    override public void PreInitialize() { }

    override public void Initialize() { }

    override public void Refresh()
    {
        if (PlayerStats.IsPlayerDead||SurvivorManager.Instance.survivor.isDead)
            LevelLost();
        else if(IsLevelWin)
            LevelWon();
    }

    override public void PhysicsRefresh()
    {
    }

    override public void EndFlow()
    {
        instance = null;
    }

    public void LevelWon()
    {
        Debug.Log("level is win");
        /*UIManager.Instance.ShowVictory();
        //Change scene
        TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            Main.Instance.ChangeCurrentFlow();
        }, 4f));*/
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
