using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour {

    public int vie = 100;
    public AudioClip soundDead;
    bool dead = false;
    
	void Start () {
        GameObject.Find("PanelUI").GetComponent<UiScript>().UpdateVie(vie);
	}	

    public void playerDegats(int degats)
    {
        vie -= degats;
        GameObject.Find("PanelUI").GetComponent<UiScript>().UpdateVie(vie);

        if(vie <=0 && !dead)
        {
            //player dead
            dead = true;
            GetComponent<AudioSource>().PlayOneShot(soundDead);
            GetComponent<deadScript>().PlayerAnimDead();
            GameObject.Find("FPSController").GetComponent<CharacterController>().enabled = false;
            GameObject.Find("BloodScreen").GetComponent<Image>().enabled = true;
            GameObject[] ennemis = GameObject.FindGameObjectsWithTag("ennemi");

            foreach (GameObject en in ennemis)
            {
                en.GetComponent<AudioSource>().enabled = false;
            }

            //Desactivation arme
            for(int i=0; i<transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("menu");
    }

    public void AddHealth(int point)
    {
        vie += point;
        GameObject.Find("PanelUI").GetComponent<UiScript>().UpdateVie(vie);
    }
}
