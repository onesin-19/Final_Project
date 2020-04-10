using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else
        {
            Time.timeScale = 1f;
        }

        isActive = !isActive;
    }
    
    public void MainMenu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
