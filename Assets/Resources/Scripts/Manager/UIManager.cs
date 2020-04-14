using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : Flow {
    #region Singleton
    static private UIManager instance = null;

    static public UIManager Instance {
        get {
            return instance ?? (instance = new UIManager());
        }
    }

    #endregion

    private LevelVariables mapVariables;

    private Text point;
    private Text ammo;
    public Text recharge;
    public Text lifeUItxt;
    public Image ImLife;
    public Text missionUI;
    public Text PseudoUI;
    public Text leaderBoard;
    public Image bloodScreen;

   /* private TextUI headshot;
    private TextUI playerKill;
    private TextUI towerKill;
    private TextUI totalKill;*/
    

    override public void PreInitialize() {
    }

    override public void Initialize() {
        
        UIVariables.uiLink = GameObject.FindObjectOfType<UIVariables>();
        point = UIVariables.uiLink.scoreUI; 
        ammo = UIVariables.uiLink.ammo; 
        recharge = UIVariables.uiLink.recharge; 
        lifeUItxt = UIVariables.uiLink.lifeUItxt; 
        ImLife = UIVariables.uiLink.ImLife; 
        missionUI = UIVariables.uiLink.missionUI; 
        PseudoUI = UIVariables.uiLink.PseudoUI;
        bloodScreen = UIVariables.uiLink.bloodScreen;
        PlayerStats.resetAllStats();
        InitScoreboard();
    }
    public void UpdateTxtCartouches (int cartouches, int maxcartouches,int chargeurs) {
        this.ammo.text = "Munitoins : " + cartouches + "/" + maxcartouches;
        this.recharge.text = "Chargeurs : " + (chargeurs>0?chargeurs:0);
        
    }

    public void UpdateLife(float vie)
    {
        vie = Mathf.Clamp(vie, 0, 100);
        ImLife.fillAmount = (float)vie / 100;
        this.lifeUItxt.text = "VIE " + vie + "%";
    }
    override public void Refresh() { }

    override public void PhysicsRefresh() { }

    override public void EndFlow() {
        instance = null;
    }

    private void InitScoreboard()
    {
        this.point.text = "Point : " + PlayerStats.Score;
        this.ammo.text = "Munitoins : " + PlayerStats.ammunition;
        this.recharge.text = "Chargeurs : " + PlayerStats.charge;
        PlayerStats.Hp = Mathf.Clamp(PlayerStats.Hp, 0, 100);
        ImLife.fillAmount = PlayerStats.Hp / 100;
        this.lifeUItxt.text = "VIE " + PlayerStats.Hp + "%";
        //this.missionUI.text = "MISSION : TROUVER LA CLEF POUR SORTIR VIVANT !!!" ;
        this.PseudoUI.text = "Pseudo : "+DB_Manager.Instance.IPseudo;
    }

    public void TurningOff() {
        this.point.gameObject.SetActive(false);
        this.ammo.gameObject.SetActive(false);
        this.recharge.gameObject.SetActive(false);
        this.lifeUItxt.gameObject.SetActive(false);
        this.missionUI.gameObject.SetActive(false);
        this.PseudoUI.gameObject.SetActive(false);
        this.leaderBoard.gameObject.SetActive(false);
        this.bloodScreen.gameObject.SetActive(false);
    }

    public void TurningOn() {
        this.point.gameObject.SetActive(true);
        this.ammo.gameObject.SetActive(true);
        this.recharge.gameObject.SetActive(true);
        this.lifeUItxt.gameObject.SetActive(true);
        this.missionUI.gameObject.SetActive(true);
        this.PseudoUI.gameObject.SetActive(true);
        this.leaderBoard.gameObject.SetActive(true);
        this.bloodScreen.gameObject.SetActive(true);
    }

    public void ShowVictory() {
        //UIVariables.uiLink.canvas.SetActive(false);
        //UIVariables.uiLink.loseUI.SetActive(false);
       //UIVariables.uiLink.winUI.SetActive(true);
        //UIVariables.uiLink.fireworks.SetActive(true);
        //UIVariables.uiLink.explosion.SetActive(false);
    }

    public void ShowBloodScreen()
    {
        bloodScreen.gameObject.SetActive(true);
    }

    public void ShowDefeat() {
        //UIVariables.uiLink.canvas.SetActive(true);
       // UIVariables.uiLink.winUI.SetActive(false);
        //UIVariables.uiLink.loseUI.SetActive(true);
        //UIVariables.uiLink.explosion.SetActive(true);
        //UIVariables.uiLink.fireworks.SetActive(false);
    }

    public void HideUI() {
        UIVariables.uiLink.canvasPlayer.SetActive(false);
        //UIVariables.uiLink.canvasMission.SetActive(false);
    }
    public void ShowUI() {
        UIVariables.uiLink.canvasPlayer.SetActive(true);
        UIVariables.uiLink.canvasMission.SetActive(true);
    }
    public void HideCanvas() {
        UIVariables.uiLink.canvas.SetActive(false);
    }

    public void ChangeStatePauseMenu(bool active)
    {
        UIVariables.uiLink.PauseMenu.SetActive(active);
    }
    public void HidePauseMenu()
    {
        UIVariables.uiLink.PauseMenu.SetActive(false);
    }
}
