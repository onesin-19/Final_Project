using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DB_Manager : MonoBehaviour {
    
    //public static DB_Manager instance;
    #region Singleton
    static private DB_Manager instance = null;

    static public DB_Manager Instance
    {
        get
        {
            if (instance == null){
                GameObject go = new GameObject();
                instance = go.AddComponent<DB_Manager>();
            }
            return instance;
        }
    }

    #endregion
    
    [Header("DATABASE")]
    public string host;
    public string database, username, password;
    MySqlConnection con;
    /*[Header("REGISTER")]
    public Canvas CanvasRegister;
    public InputField RPseudo;
    public InputField RPassword, Rnom, Rprenom, REmail;
    public Text RtxtInfos;  
    [Header("LOGIN")]
    public Canvas CanvasLogin;
    public InputField LPseudo;
    public InputField LPassword;
    public Text LtxtInfos;*/
    [Header("INFO USER")]
    public string IPseudo;
    public string INom, IPrenom, IEmail;
    public int IPoints;
    [Header("All Connected Users")]
    public Users connectUsers;
    private void Start()
    {
        //connect_BDD();
    }

    void Awake () {
        
        if (instance !=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

	}

    public void connect_BDD()
    {
        //string cmd = "SERVER=" + host + ";" + "database =" + database + ";User ID=" + username + ";Password=" + password + ";Pooling=true;Charset=utf8";
        string cmd = "server=" + host + ";" + "database =" + database + ";uid=" + username + ";pwd=" +
                     password;
       
        MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        conn_string.Server = host;
        conn_string.Port = 3307;
        conn_string.UserID = username;
        conn_string.Password = password;
        conn_string.Database = database;

        try
        {
            con = new MySqlConnection(conn_string.ToString());
            
            con.Open();
            Debug.Log(con.State);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }
	
	void Update()
    {
        //Debug.Log(con.State);
    }

    bool IsValidLenght(string InputString,int LenghtString)
    {
        if(InputString.Length>LenghtString)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    bool IsValidEmail(string InputEmail)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(InputEmail);
        if(match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }                
    }


    bool IsValid()
    {
        //Verification de l'Email
        ColorBlock cbEmail = ConnectionVariables.instance.REmail.colors;

        if (!IsValidEmail(ConnectionVariables.instance.REmail.text))
        {
            ConnectionVariables.instance.RtxtInfos.text = "Invalid Email";
            cbEmail.normalColor = Color.red;
            ConnectionVariables.instance.REmail.colors = cbEmail;
            return false;
        }
        else
        {
            ConnectionVariables.instance.RtxtInfos.text = "";
            cbEmail.normalColor = Color.white;
            ConnectionVariables.instance.REmail.colors = cbEmail;
        }

        //Pseudo
        ColorBlock cbPseudo = ConnectionVariables.instance.RPseudo.colors;

        if (!IsValidLenght(ConnectionVariables.instance.RPseudo.text, 5))
        {
            cbPseudo.normalColor = Color.red;
            ConnectionVariables.instance.RtxtInfos.text = "Pseudo Invalid";
            ConnectionVariables.instance.RPseudo.colors = cbPseudo;
            return false;
        }
        else
        {
            cbPseudo.normalColor = Color.white;
            ConnectionVariables.instance.RtxtInfos.text = "";
            ConnectionVariables.instance.RPseudo.colors = cbPseudo;
        }

        //Password
        ColorBlock cbPassword = ConnectionVariables.instance.RPassword.colors;

        if (!IsValidLenght(ConnectionVariables.instance.RPassword.text, 5))
        {
            cbPassword.normalColor = Color.red;
            ConnectionVariables.instance.RtxtInfos.text = "Password Invalid";
            ConnectionVariables.instance.RPassword.colors = cbPassword;
            return false;
        }
        else
        {
            cbPassword.normalColor = Color.white;
            ConnectionVariables.instance.RtxtInfos.text = "";
            ConnectionVariables.instance.RPassword.colors = cbPseudo;
        }

        //Other

        if(!IsValidLenght(ConnectionVariables.instance.Rnom.text,0) || !IsValidLenght(ConnectionVariables.instance.Rprenom.text,0))
        {
            ConnectionVariables.instance.RtxtInfos.text = "Empty not autorized";
            return false;
        }

        //Verification existance pseudo

        try
        {
            connect_BDD();
            MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `utilisateurs` WHERE `pseudo`='" + ConnectionVariables.instance.RPseudo.text + "'", con);
            MySqlDataReader MyReader = CmdSql.ExecuteReader();
            string data = null;

            while (MyReader.Read())
            {
                data = MyReader["password"].ToString();

                if(data !=null)
                {
                    ConnectionVariables.instance.RtxtInfos.text = "Pseudo Exist";
                    MyReader.Close();
                    return false;
                }
            }
            MyReader.Close();

        }
        catch (Exception Ex) { Debug.Log(Ex.ToString()); }

        ConnectionVariables.instance.RtxtInfos.text = null;
        return true;
    }


    public void Register()
    {
        if(IsValid())
        {
            
            string cmd = "INSERT INTO `utilisateurs` (`id`, `pseudo`, `password`, `nom`, `prenom`, `email`, `points`) VALUES(NULL, '" + 
                         ConnectionVariables.instance.RPseudo.text + "', '"+ Md5Sum(ConnectionVariables.instance.RPassword.text)+"', '"+
                         ConnectionVariables.instance.Rnom.text+"', '"+ConnectionVariables.instance.Rprenom.text+"', '"+ ConnectionVariables.instance.REmail.text+"', '0')";
            MySqlCommand CmdSql = new MySqlCommand(cmd, con);

            try
            {
                CmdSql.ExecuteReader();
                ConnectionVariables.instance.RtxtInfos.text = "Register Succesfull";
            }
            catch (Exception Ex)
            {
                ConnectionVariables.instance.RtxtInfos.text = Ex.ToString();
            }

        }
        else
        {
            Debug.Log("non valide");
        }        
    }

    string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public void Login()
    {
        try
        {
            connect_BDD();
            MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `utilisateurs` WHERE `pseudo`='" + ConnectionVariables.instance.LPseudo.text + "'", con);
            MySqlDataReader MyReader = CmdSql.ExecuteReader();
            string data = null;

            while (MyReader.Read())
            {
                data = MyReader["password"].ToString();

                if (data == Md5Sum(ConnectionVariables.instance.LPassword.text))
                {
                    ConnectionVariables.instance.LtxtInfos.text = "Login Successfull";
                    //recupération des données utilisateur
                    IPrenom = MyReader["prenom"].ToString();
                    INom= MyReader["nom"].ToString();
                    IEmail= MyReader["email"].ToString();
                    IPseudo= MyReader["pseudo"].ToString();
                    IPoints = (int)MyReader["points"];
                    SceneManager.LoadScene("menu");
                    User user=new User(IPseudo,true);
                    if(!connectUsers.checkPlayer(IPseudo))
                        connectUsers.users.Add(user);
                    else
                    {
                        connectUsers.activePlayer(IPseudo);
                    }
                }
                else
                {
                    ConnectionVariables.instance.LtxtInfos.text = "Wrong Login or Password";
                }
            }

            if(data==null)
            {
                ConnectionVariables.instance.LtxtInfos.text = "Account not existing";
            }
            MyReader.Close();

        }
        catch (Exception Ex) { Debug.Log(Ex.ToString()); }
    }

    public void ShowRegister()
    {
        ConnectionVariables.instance.CanvasLogin.gameObject.SetActive(false);
        ConnectionVariables.instance.CanvasRegister.gameObject.SetActive(true);
    }

    public void ShowLogin()
    {
        ConnectionVariables.instance.CanvasLogin.gameObject.SetActive(true);
        ConnectionVariables.instance.CanvasRegister.gameObject.SetActive(false);
    }

    public void savePoints()
    {
        string cmd = "UPDATE `utilisateurs` SET `points`=" + IPoints + " WHERE `pseudo`='" + IPseudo + "'";
        MySqlCommand CmdSql = new MySqlCommand(cmd, con);

        try
        {
            CmdSql.ExecuteReader();
            Debug.Log("Update Successfull");
        }
        catch (Exception Ex)
        {
            Debug.Log(Ex.ToString());
        }
    }

    public string LeaderBoard(int Limit)
    {
        try
        {
            connect_BDD();
            MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `utilisateurs` order by `points` DESC LIMIT " + Limit, con);
            MySqlDataReader MyReader = CmdSql.ExecuteReader();

            string data = null;
            while (MyReader.Read())
            {
                data += MyReader["pseudo"].ToString() + ":" + MyReader["points"] + "\n";
            }
            MyReader.Close();
            return data;
        }
        catch 
        {
            return null;
        }
    }

    public void GetConnectUser()
    {
        
    }

    public void deconnectUser()
    {
        SceneManager.LoadScene("login");
        connectUsers.users.Remove(connectUsers.getPlayUser());
    }
    void OnApplicationQuit()
    {
        foreach (User u in connectUsers.users)
        {
            u.isPlay = false;
        }
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
    
}
