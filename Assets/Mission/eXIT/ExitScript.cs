using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

    //public string LevelToLoad;
    public AudioClip SoundWin;

	void OnTriggerEnter (Collider col) {

        if(col.gameObject.tag=="Player")
        {
            bool locked = UIVariables.uiLink.canvasMission.GetComponent<MissionScript>().LockedDoor;
            if(locked)
            {
               
                UIManager.Instance.ShowMissionCanvas();
                UIManager.Instance.missionUI.text= "PORTE VEROULLIE...";
                UIVariables.uiLink.canvasMission.GetComponent<MissionScript>().DesactiveTxt();
                
            }
            else
            {
                UIManager.Instance.ShowMissionCanvas();
                UIManager.Instance.missionUI.text= "MISSION ACCOMPLIE...";
                UIVariables.uiLink.canvasMission.GetComponent<MissionScript>().DesactiveTxt();
                col.gameObject.GetComponent<AudioSource>().PlayOneShot(SoundWin);
                ///StartCoroutine(ChargementScene());
                LogicManager.Instance.IsLevelWin = true;
            }
        }		
	}

    /*IEnumerator ChargementScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(LevelToLoad);
    }*/
	
	
}
