using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScriptMenu : MonoBehaviour
{
    public Text txtleaderboard;
    public GameObject panelButton;
    public GameObject panelBoard;
    public GameObject leaderboradBtn;
    public GameObject ReturnBtn;
    public GameObject panelControl;
    public GameObject clavierPanel;
    public GameObject controlBtn;
    public Text txtTitle;
    
    public void PlayTheGame()
    {
        //SceneManager.LoadScene("level1");
        PlayerStats.resetAllStats();
        GameVariables.Instance.sceneFader.FadeTo("level1");
    }

    public void ExitGame()
    {
        if(!Application.isEditor) { System.Diagnostics.Process.GetCurrentProcess().Kill(); }
        //Application.Quit();
    }

    public void Deconnect()
    {
        DB_Manager.Instance.deconnectUser();
    }

    public void Leaderboard()
    {
        string data = DB_Manager.Instance.LeaderBoard(5);
        panelBoard.SetActive(true);
        panelButton.SetActive(false);
        EventSystem.current.SetSelectedGameObject(ReturnBtn);

        txtleaderboard.text = data;
    }
    public void Retour()
    {
        panelBoard.SetActive(false);
        panelButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(leaderboradBtn);


    }

    public void RetourControl()
    {
        panelControl.SetActive(false);
        panelButton.SetActive(true);
        txtTitle.text = "Survivor Zombie";
        EventSystem.current.SetSelectedGameObject(controlBtn);


    }

    public void Controle()
    {
        panelControl.SetActive(true);
        panelButton.SetActive(false);
        txtTitle.text = "Options";
        EventSystem.current.SetSelectedGameObject(clavierPanel);
    }

}
