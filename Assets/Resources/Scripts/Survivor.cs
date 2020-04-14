﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    public float minSpeed = 7f, maxSpeed = 8f;
    private float health;
    public float startHealth = 5;
    public bool isDead;
    private static readonly int Run = Animator.StringToHash("run");
    private Rigidbody rb;
    
    public void Initialize()
    {
        if (SurvivorManager.Instance.waypoints.Length > 0)
            target = SurvivorManager.Instance.waypoints[0];
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = Random.Range(minSpeed,maxSpeed);
        health = startHealth;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Refresh()
    {
        if( agent.speed > 0)
            anim.SetBool(Run, true);
        //si atteint le dernier waypoint ou tres loin du player
        if (Vector3.Distance(transform.position,SurvivorManager.Instance.waypoints[SurvivorManager.Instance.waypoints.Length - 1].position)<5||
            Vector3.Distance(transform.position,PlayerManager.Instance.player.transform.position)>10)
        {
            anim.SetBool(Run, false);
            agent.speed = 0;
        }
        else
        {
            anim.SetBool(Run, true);
            agent.speed = Random.Range(minSpeed,maxSpeed);
        }
        
        if (SurvivorManager.Instance.waypoints.Length > 0)
        {

            if (Vector3.Distance(transform.position, target.position) <= 2f)
            {
                //check the next way point
                if (waypointIndex >= SurvivorManager.Instance.waypoints.Length-1)
                {
                    if (Vector3.Distance(PlayerManager.Instance.player.transform.position,SurvivorManager.Instance.waypoints[SurvivorManager.Instance.waypoints.Length - 1].position)<5)
                    {
                        LogicManager.Instance.IsLevelWin = true;
                    }
                    return;
                }

                waypointIndex++;
                target = SurvivorManager.Instance.waypoints[waypointIndex];
            }
            
           
        }
    }

    public void PhysicsRefresh() {
        if (SurvivorManager.Instance.waypoints.Length > 0)
        {
            agent.SetDestination(target.position);
            /*Vector3 dir = target.position - transform.position;
            transform.LookAt(target);
            
            rb.MovePosition((dir.normalized )* (5 * Time.fixedDeltaTime) + rb.position);*/
            
        }

    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0 && !isDead)
        {
            Dead();
        }
        Debug.Log("health: "+health);
    }
    
    public void Dead () {
        isDead = true;
        gameObject.tag = "Untagged";
        GetComponent<Rigidbody>().isKinematic = false;
        anim.SetTrigger("dead");
        anim.SetBool("run", false);
        GetComponent<Survivor>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;     
        //AudioEnnemi.GetComponent<AudioSource>().Stop();
        //AudioEnnemi.GetComponent<AudioSource>().PlayOneShot(soundDead);
    }
   
}
