using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour {

    public AudioClip soundkey;
	void OnTriggerEnter (Collider col) {

        if(col.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(soundkey);
            UIVariables.uiLink.canvasMission.GetComponent<MissionScript>().LockedDoor = false;
            UIVariables.uiLink.canvasMission.GetComponent<MissionScript>().PanelTexte.SetActive(true);
            UIManager.Instance.missionUI.text = "TROUVER LA SORTIE...";
            GameObject.Find("CanvasMission").GetComponent<MissionScript>().DesactiveTxt();
            Destroy(gameObject, 0.5f);

        }  
	}

   
	
	
}
