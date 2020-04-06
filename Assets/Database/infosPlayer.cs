using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infosPlayer : MonoBehaviour {

    public Text txtPseudo, txtPoints, txtleaderboard;
    //DB_Manager manager;

	// Use this for initialization
    void Start()
    {
        //manager = GameObject.Find("MySqlManager").GetComponent<DB_Manager>();
        txtPseudo.text = "Pseudo " + DB_Manager.Instance.IPseudo;
        txtPoints.text = "Points " + DB_Manager.Instance.IPoints;
    }

    public void addPoints () {
        DB_Manager.Instance.IPoints += 10;
        txtPoints.text = "Points " + DB_Manager.Instance.IPoints;
    }

    public void SavePoints()
    {
        DB_Manager.Instance.savePoints();
    }

    public void LeaderBoard()
    {
        string data = DB_Manager.Instance.LeaderBoard(5);
        txtleaderboard.text = data;
    }

    public void deconnect()
    {
        DB_Manager.Instance.deconnectUser();
    }

   
}
