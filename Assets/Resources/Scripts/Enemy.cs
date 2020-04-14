using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

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
    
    public GameObject AudioEnnemi;
    public AudioClip soundDead;
    public bool isDead = false;
    
    public GameObject bloodEffect;
    private float health;
    public float startHealth = 5;
    public void Initialize () {
        health = startHealth;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = Random.Range(minSpeed,maxSpeed);
        target = PlayerManager.Instance.player;//GameObject.Find("FPSController");
    }
	
	
    public void Refresh () {
        if(isDead)
        {
            float x, y;
            x = GetComponent<CapsuleCollider>().center.x;
            y = GetComponent<CapsuleCollider>().center.y;
            GetComponent<CapsuleCollider>().center = new Vector3(x, y, anim.GetFloat("colz"));
            //return;
        } 
        
        if(Vector3.Distance(PlayerManager.Instance.player.transform.position,transform.position)<=Vector3.Distance(SurvivorManager.Instance.survivor.transform.position,transform.position))
            target = PlayerManager.Instance.player;
        else
        {
            target = SurvivorManager.Instance.survivor.gameObject;
        }
        
        distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance<walkDistance)
        {
            anim.SetBool("walk", true);
            anim.SetBool("attack", false);
            agent.SetDestination(target.transform.position);
            if(distance<attackDistance)
            {
                anim.SetBool("attack", true);
                transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.transform.position-transform.position),2*Time.deltaTime);
                
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            anim.SetBool("walk",false);
            anim.SetBool("attack",false);
            agent.SetDestination(transform.position);
        } 
        
        
    }

    public void PhysicsRefresh()
    {
        
    }
    public void damageToPlayer()
    {
        
        GetComponent<AudioSource>().PlayOneShot(soundAttack);
        
        //GameObject.Find("FirstPersonCharacter").GetComponent<HealthScript>().playerDegats(Damage);
        if (target==PlayerManager.Instance.player)
        {
            PlayerManager.Instance.playerDegats(Damage);
        }
        else if (target==SurvivorManager.Instance.survivor.gameObject)
        {
            SurvivorManager.Instance.SurvivorDegats(Damage);
        }

    }
    
    public void ennemiDead () {
        isDead = true;
        gameObject.tag = "Untagged";
        GetComponent<Rigidbody>().isKinematic = false;
        anim.SetTrigger("dead");
        anim.SetBool("attack", false);
        GetComponent<Enemy>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;     
        AudioEnnemi.GetComponent<AudioSource>().Stop();
        AudioEnnemi.GetComponent<AudioSource>().PlayOneShot(soundDead);
        EnemyManager.Instance.EnemyDied(this);
    }
    public void TakeDamage(float amount)
    {
        
        health -= amount;
        //anim.SetBool("iswalk", false);
        //anim.SetBool("isHit", true);

        //GameObject bEffect = (GameObject)Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //Destroy(bEffect, 2f);

        if (health <= 0 && !isDead)
        {
            ennemiDead();
        }
    }
    public void activationIskinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}