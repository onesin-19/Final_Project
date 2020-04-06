using UnityEngine;
using System.Collections;

public class IAParasite : MonoBehaviour {

    public GameObject target;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    public float walkDistance = 10f;
    public float attackDistance = 2f;
    [SerializeField]
    private float distance;
    public int Damage = 10;
    public AudioClip soundAttack;
    public float minSpeed = 1f, maxSpeed = 2f;
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = Random.Range(minSpeed,maxSpeed);
        target = GameObject.Find("FPSController");
    }
	
	
	void Update () {

        distance = Vector3.Distance(target.transform.position, transform.position);

        if(distance<walkDistance)
        {
            anim.SetBool("walk", true);
            anim.SetBool("attack", false);
            agent.SetDestination(target.transform.position);

            if(distance<attackDistance)
            {
                anim.SetBool("attack", true);
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("attack", false);
            agent.SetDestination(transform.position);
        }        
	}

    public void damageToPlayer()
    {
        GetComponent<AudioSource>().PlayOneShot(soundAttack);
        GameObject.Find("FirstPersonCharacter").GetComponent<HealthScript>().playerDegats(Damage);
        Debug.Log("Player hit");
    }
}
