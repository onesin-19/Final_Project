using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New User", menuName = "Users")]
public class Users : ScriptableObject
{
    
    public List<User> users=new List<User>();

    public User getPlayUser()
    {
        User user = null;
        int i = 0;
        while (i<users.Count&&user==null)
        {
            if (users[i].isPlay)
                user = users[i];
            i++;
        }
        return user;
    }

    public void activePlayer(string pseudo)
    {
        foreach (User u in users)
        {
            if (u.pseudo==pseudo)
                u.isPlay=true;
        }
    }

    public bool checkPlayer(string pseudo)
    {
        bool contains = false;
        int i = 0;
        while (i<users.Count&&!contains)
        {
            if (users[i].pseudo == pseudo)
                contains = true;
            i++;
        }
        
        return contains;
    }
}

[System.Serializable]

public class User
{
    public string pseudo;
    public bool isPlay;

    public User(string ps, bool isPlay)
    {
        pseudo = ps;
        this.isPlay = isPlay;
    }
}