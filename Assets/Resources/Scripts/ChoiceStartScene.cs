using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceStartScene : MonoBehaviour
{
    public Users connectUsers;
    // Start is called before the first frame update
    public GameObject confirmbox;
    public Dropdown dropdownbox;
    public GameObject soldat;
    void Start()
    {
        JsonListWrapper<User> us=JsonManager.JsonToStringList<User>("jsonSaveData.json");
        connectUsers.users = us.list;
        DB_Manager.Instance.connectUsers = connectUsers;
        if (connectUsers.users.Count==0)
        {
            DB_Manager.Instance.connect_BDD();
            soldat.SetActive(true);
            GameVariables.Instance.sceneFader.FadeTo("login");
        }
        /*else if(connectUsers.users.Count==1)
        {
            //SceneManager.LoadScene("game");
            //dropdownbox.transform.parent.gameObject.SetActive(false);
            //connectUsers.users[0].isPlay = true;
        }*/
        else
        {
            confirmbox.SetActive(true);
            //confirmbox.SetActive(false);
            //dropdownbox.transform.parent.gameObject.SetActive(true);
            List<string> options =new List<string>();
            foreach (User u in connectUsers.users)
            {
                options.Add(u.pseudo);
            }
            dropdownbox.options.Clear();
            dropdownbox.AddOptions(options);

            
        }
    }
    public void confirmConnectionNO()
    {
        DB_Manager.Instance.connect_BDD();
        GameVariables.Instance.sceneFader.FadeTo("login");
        //SceneManager.LoadScene("login");
    }
    public void confirmConnectionYES()
    {
        DB_Manager.Instance.IPseudo = dropdownbox.options[dropdownbox.value].text;
        GameVariables.Instance.sceneFader.FadeTo("menu");
        //SceneManager.LoadScene("menu");
        connectUsers.activePlayer(dropdownbox.options[dropdownbox.value].text);//.users[0].isPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
