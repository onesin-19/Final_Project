using UnityEngine;
using System.Collections;

public class ArmeManager : MonoBehaviour {

    public GameObject[] arme;
    public int current_arme = 1;
    public float delai = 1f;
	
	void Start () {
	
        for(int i=0; i<arme.Length; i++)
        {
            if(i!=current_arme)
            {
                arme[i].SetActive(false);
            }
            else
            {
                arme[i].SetActive(true);
            }
        }

	}
	
	
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Alpha1)||Input.GetButton("PS4_L1"))
        {
            if(current_arme!=0)
            {
                StartCoroutine(transition(arme[current_arme], arme[0]));
                current_arme = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)||Input.GetButton("PS4_R1"))
        {
            if (current_arme != 1)
            {
                StartCoroutine(transition(arme[current_arme], arme[1]));
                current_arme = 1;
            }
        }       

    }

    IEnumerator transition(GameObject current, GameObject newarme)
    {
        current.GetComponent<Animator>().SetTrigger("out");
        yield return new WaitForSeconds(delai);
        current.SetActive(false);
        newarme.SetActive(true);
    }
}
