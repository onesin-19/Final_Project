using UnityEngine;
using System.Collections;

public class dead : MonoBehaviour {

    private Animator anim;
    public GameObject AudioEnnemi;
    public AudioClip soundDead;
    bool isDead = false;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
    void Update()
    {
        if(isDead)
        {
            float x, y;
            x = GetComponent<CapsuleCollider>().center.x;
            y = GetComponent<CapsuleCollider>().center.y;
            GetComponent<CapsuleCollider>().center = new Vector3(x, y, anim.GetFloat("colz"));
        }                
        
    }

	
	public void ennemiDead () {
        isDead = true;
        gameObject.tag = "Untagged";
        GetComponent<Rigidbody>().isKinematic = false;
        anim.SetTrigger("dead");
        anim.SetBool("attack", false);
        GetComponent<IAParasite>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;     
        AudioEnnemi.GetComponent<AudioSource>().Stop();
        AudioEnnemi.GetComponent<AudioSource>().PlayOneShot(soundDead);
        Destroy(gameObject, 10f);	
	}

    public void activationIskinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
