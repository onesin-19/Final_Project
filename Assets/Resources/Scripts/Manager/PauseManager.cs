using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
            PlayerManager.Instance.player.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            PlayerManager.Instance.player.transform.GetChild(0).gameObject.SetActive(true);
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
        PlayerManager.Instance.player.transform.GetChild(0).gameObject.SetActive(false);
        sceneFader.FadeTo(menuSceneName);
    }
}
