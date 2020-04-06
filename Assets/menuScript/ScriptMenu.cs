using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour {

	public void PlayTheGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Deconnect()
    {
        DB_Manager.Instance.deconnectUser();
    }

}
