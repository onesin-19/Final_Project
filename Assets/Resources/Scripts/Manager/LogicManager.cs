using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

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
    public bool IsLevelLost { get; set; }

    override public void PreInitialize() { }

    override public void Initialize() { }

    override public void Refresh()
    {
        if (PlayerStats.IsPlayerDead||!PlayerStats.HasSaveSurvivor||IsLevelLost/*SurvivorManager.Instance.survivor.isDead*/)
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
       
        Main.Instance.StartCoroutine(GameOver(true));
        /*UIManager.Instance.ShowVictory();
        //Change scene
        TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            Main.Instance.ChangeCurrentFlow();
        }, 4f));*/
    }

    IEnumerator GameOver(bool isWin)
    {
        yield return new WaitForSeconds(3f);
        Main.Instance.ChangeCurrentFlow(isWin);
    }
    public void LevelLost()
    {
        UIManager.Instance.ShowMissionCanvas();
        UIManager.Instance.missionUI.text= "ECHEC MISSION...";
        Main.Instance.StartCoroutine(GameOver(false));
        //UIManager.Instance.ShowBloodScreen();
        /*TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            Main.Instance.ChangeCurrentFlow();
        }, 3f));*/
    }
    
}
