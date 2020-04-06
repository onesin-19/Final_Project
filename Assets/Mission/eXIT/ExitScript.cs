using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

    public string LevelToLoad;
    public AudioClip SoundWin;

	void OnTriggerEnter (Collider col) {

        if(col.gameObject.tag=="Player")
        {
            bool locked = GameObject.Find("CanvasMission").GetComponent<MissionScript>().LockedDoor;
            if(locked)
            {
               
                GameObject.Find("CanvasMission").GetComponent<MissionScript>().PanelTexte.SetActive(true);
                GameObject.Find("CanvasMission").GetComponent<MissionScript>().PanelTexte.transform.Find("Text").GetComponent<Text>().text = "PORTE VEROULLIE...";
                GameObject.Find("CanvasMission").GetComponent<MissionScript>().DesactiveTxt();
            }
            else
            {
                GameObject.Find("CanvasMission").GetComponent<MissionScript>().PanelTexte.SetActive(true);
                GameObject.Find("CanvasMission").GetComponent<MissionScript>().PanelTexte.transform.Find("Text").GetComponent<Text>().text = "MISSION ACCOMPLIE...";
                GameObject.Find("CanvasMission").GetComponent<MissionScript>().DesactiveTxt();
                GetComponent<AudioSource>().PlayOneShot(SoundWin);
                StartCoroutine(ChargementScene());
            }
        }		
	}

    IEnumerator ChargementScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(LevelToLoad);
    }
	
	
}
