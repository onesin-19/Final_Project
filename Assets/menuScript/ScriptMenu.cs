using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptMenu : MonoBehaviour
{
    public Text txtleaderboard;
    public GameObject panelButton;
    public GameObject panelBoard;
    
    public void PlayTheGame()
    {
        //SceneManager.LoadScene("level1");
        GameVariables.Instance.sceneFader.FadeTo("level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Deconnect()
    {
        DB_Manager.Instance.deconnectUser();
    }

    public void Leaderboard()
    {
        string data = DB_Manager.Instance.LeaderBoard(5);
        /*GameObject.Find("leaderboardPanel")*/panelBoard.SetActive(true);
        /*GameObject.Find("buttonPanel")*/panelButton.SetActive(false);

        txtleaderboard.text = data;
    }
    public void Retour()
    {
        /*GameObject.Find("leaderboardPanel")*/panelBoard.SetActive(false);
        /*GameObject.Find("buttonPanel")*/panelButton.SetActive(true);

    }

}
