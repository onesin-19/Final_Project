using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour {

    public AudioClip soundkey;
	void OnTriggerEnter (Collider col) {

        if(col.gameObject.tag=="Player")
        {
            GetComponent<AudioSource>().PlayOneShot(soundkey);
            GameObject.Find("CanvasMission").GetComponent<MissionScript>().LockedDoor = false;
            GameObject.Find("CanvasMission").GetComponent<MissionScript>().PanelTexte.SetActive(true);
            //GameObject.Find("CanvasMission").GetComponent<MissionScript>().PanelTexte.transform.FindChild("Text").GetComponent<Text>().text = "TROUVER LA SORTIE...";
            UIManager.Instance.missionUI.text = "TROUVER LA SORTIE...";
            GameObject.Find("CanvasMission").GetComponent<MissionScript>().DesactiveTxt();
            Destroy(gameObject, 0.5f);

        }  
	}

   
	
	
}
