using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoScript : MonoBehaviour {
    public AudioClip soundAmmo;
    public int nbChargeur = 1;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(AddAmmo());
        }
    }

    IEnumerator AddAmmo()
    {
        GetComponent<AudioSource>().PlayOneShot(soundAmmo);

        int indexTab = GameObject.Find("FirstPersonCharacter").GetComponent<ArmeManager>().current_arme;
        GameObject GoArme = GameObject.Find("FirstPersonCharacter").GetComponent<ArmeManager>().arme[indexTab];
        GoArme.GetComponent<Tir>().AddChargeurs(nbChargeur);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
