using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PauseManager : Flow
{
    
    #region Singleton
    static private PauseManager instance = null;

    static public PauseManager Instance {
        get {
            return instance ?? (instance = new PauseManager());
        }
    }

    #endregion

    private bool isActive = false;
    public SceneFader sceneFader;

    public string menuSceneName = "Menu";
    override public void PreInitialize()
    {
        
    }

    override public void Initialize()
    {
        sceneFader = LevelVariables.instance.sceneFader;
    }

    override public void Refresh()
    {
        if (Input.GetKeyDown(KeyCode.P)||Input.GetButtonDown("PS4_Option"))
        {
            Toggle();
        }
    }

    override public void PhysicsRefresh()
    {
    }

    override public void EndFlow()
    {
        instance = null;
    }
    public void Toggle()
    {
        UIManager.Instance.ChangeStatePauseMenu(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0f;
            PlayerManager.Instance.player.GetComponent<FirstPersonController>().UnlockMouse();
            PlayerManager.Instance.player.GetComponent<FirstPersonController>().enabled=false;
            PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Tir>().enabled=false;
            PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<AnimArme1>().enabled=false;
            PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Tir>().enabled=false;
            PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<AnimArme2>().enabled=false;

        }
        else
        {
            TimeManager.Instance.AddTimedAction(new TimedAction(() =>
            {
                PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Tir>().enabled=true;
                PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<AnimArme1>().enabled=true;
                PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Tir>().enabled=true;
                PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<AnimArme2>().enabled=true;
            }, Time.deltaTime));
            PlayerManager.Instance.player.GetComponent<FirstPersonController>().enabled=true;
            Time.timeScale = 1f;
        }

        isActive = !isActive;
    }
    
    public void MainMenu()
    {
        Toggle();
        PlayerManager.Instance.player.GetComponent<FirstPersonController>().UnlockMouse();
        PlayerManager.Instance.player.GetComponent<FirstPersonController>().enabled=false;
        PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Tir>().enabled=false;
        PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<AnimArme1>().enabled=false;
        PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Tir>().enabled=false;
        PlayerManager.Instance.player.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<AnimArme2>().enabled=false;
        DB_Manager.Instance.savePoints();
        sceneFader.FadeTo(menuSceneName);
    }
}
