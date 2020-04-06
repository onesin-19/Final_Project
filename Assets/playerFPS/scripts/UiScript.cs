using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiScript : MonoBehaviour {

    public Text TxtMunitions, TxtChargeurs,TxtVie ;
    public Image ImVie;
    		
	public void UpdateTxtCartouches (int cartouches, int maxcartouches,int chargeurs) {

        TxtMunitions.text = "MUNITIONS " + cartouches + "/" + maxcartouches;
        TxtChargeurs.text = "CHARGEURS " + chargeurs;
	}

    public void UpdateVie(int vie)
    {
        vie = Mathf.Clamp(vie, 0, 100);
        ImVie.fillAmount = (float)vie / 100;
        TxtVie.text = "VIE " + vie + "%";
    }
}
