using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionScript : MonoBehaviour {

    public GameObject PanelTexte;
    public bool LockedDoor;
	void Start () {
		LockedDoor = true;
        DesactiveTxt();
	}
	
	
	public void DesactiveTxt () {
        StartCoroutine(DesactivationPanel());
    }

    IEnumerator DesactivationPanel()
    {
        yield return new WaitForSeconds(5f);
        PanelTexte.SetActive(false);
    }

}
