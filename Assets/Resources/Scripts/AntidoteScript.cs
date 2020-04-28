using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AntidoteScript : MonoBehaviour
{
    public AudioClip sound;
	
    void OnTriggerEnter (Collider col) {
        if(col.gameObject.tag=="Player")
        {
            StartCoroutine(UpdateStatPlayer());
        }	
    }
	
    IEnumerator UpdateStatPlayer()
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
        PlayerStats.NbAntidote++;
        UIManager.Instance.UpdateNbAtidote();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
