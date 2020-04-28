using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : Flow {
    #region Singleton
    static private PlayerManager instance = null;

    static public PlayerManager Instance {
        get {
            return instance ?? (instance = new PlayerManager());
        }
    }

    #endregion

    public GameObject player;
    //public bool changeScene;
    override public void PreInitialize()
    {
        //changeScene = false;
        //player = (GameObject.Instantiate(Resources.Load("Prefabs/Player/FPSController")) as GameObject).GetComponent<Player>();
        /*if (Main.Instance.isInRoomScene) {
            player.transform.position = player.startingRoomPos;
        }
        else {
            player.transform.position = player.startingMapPos;
        }*/

        //Main.Instance.VRPlayerCharacter = player.gameObject;
        //player.PreInitialize();
    }

    override public void Initialize() {
        //mainPlayerController.Initialize();
        //player.Initialize();
        player=GameObject.Instantiate(Resources.Load("Prefabs/Player/FPSController"),LevelVariables.instance.playerSpawnPosition.position,Quaternion.identity)as GameObject;
        //PlayerStats.resetAllStats();

    }

    override public void Refresh() {
        
    }

    override public void PhysicsRefresh() {
        //player.PhysicsRefresh();
        //mainPlayerController.PhysicsRefresh();
        //changeLife();
    }

    override public void EndFlow() {
        //instance = null;
    }
    private void changeLife()
    {
        if (UIVariables.uiLink.lifeUItxt.text != PlayerStats.Hp.ToString())
        {
            UIVariables.uiLink.lifeUItxt.text =  PlayerStats.Hp+" %";
        }
    }
    
    public void playerDegats(int Damage)
    {
        if (!PlayerStats.IsPlayerDead&&!LogicManager.Instance.IsLevelWin&&!LogicManager.Instance.IsLevelLost)
        {
            PlayerStats.subtractHp(Damage);
        
            UIManager.Instance.UpdateLife(PlayerStats.Hp);

        }
       
        if(PlayerStats.Hp <=0 && !PlayerStats.IsPlayerDead)
        {
            //player dead
            PlayerStats.IsPlayerDead = true;
            Transform firstChild = player.transform.GetChild(0);
            firstChild.GetComponent<AudioSource>().PlayOneShot(LevelVariables.instance.soudDead);
            firstChild.GetComponent<Animator>().enabled=true;
            player.GetComponent<CharacterController>().enabled = false;
            UIManager.Instance.ShowBloodScreen();
            GameObject[] ennemis = GameObject.FindGameObjectsWithTag("ennemi");

            foreach (GameObject en in ennemis)
            {
                en.GetComponent<AudioSource>().enabled = false;
            }

            //Desactivation arme
            for(int i=0; i<player.transform.childCount;i++)
            {
                firstChild.transform.GetChild(i).gameObject.SetActive(false);
            }

            player.GetComponent<FirstPersonController>().UnlockMouse();
            player.GetComponent<FirstPersonController>().enabled = false;
        }
    }
}
