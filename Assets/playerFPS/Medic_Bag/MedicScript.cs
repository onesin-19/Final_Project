using UnityEngine;
using System.Collections;

public class MedicScript : MonoBehaviour {

    public AudioClip soundMedic;
    public int medicPoint = 10;
	
	void OnTriggerEnter (Collider col) {
        if(col.gameObject.tag=="Player")
        {
            StartCoroutine(AddMedicPlayer());
        }	
	}
	
    IEnumerator AddMedicPlayer()
    {
        GetComponent<AudioSource>().PlayOneShot(soundMedic);
        PlayerStats.addHp(medicPoint);
        UIManager.Instance.UpdateLife(PlayerStats.Hp);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
	
}
