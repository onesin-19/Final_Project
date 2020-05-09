using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConnectionVariables : MonoBehaviour
{
    #region Singleton
    public static ConnectionVariables instance;
    private void Awake() {

        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
    #endregion
 
    [Header("REGISTER")]
    public Canvas CanvasRegister;
    public InputField RPseudo;
    public InputField RPassword, Rnom, Rprenom, REmail;
    public Text RtxtInfos;  
    [Header("LOGIN")]
    public Canvas CanvasLogin;
    public InputField LPseudo;
    public InputField LPassword;
    public Text LtxtInfos;
    public GameObject RegisterBtn;
    public GameObject LoginBtn;

    public void login()
    {
        DB_Manager.Instance.Login();
    }

    public void Showlogin()
    {
        DB_Manager.Instance.ShowLogin();
        EventSystem.current.SetSelectedGameObject(RegisterBtn);
    }
    
    public void Register()
    {
        DB_Manager.Instance.Register();
    }
    
    public void ShowRegister()
    {
        DB_Manager.Instance.ShowRegister();
        EventSystem.current.SetSelectedGameObject(LoginBtn);
    }

    public void Quit()
    {
        if(!Application.isEditor) { System.Diagnostics.Process.GetCurrentProcess().Kill(); }
        //Application.Quit();
    }
}
