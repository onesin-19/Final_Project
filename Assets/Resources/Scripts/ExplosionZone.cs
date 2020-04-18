using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionZone : MonoBehaviour
{
    public GameObject explosionPrefabs;
    public int Force;
    public int radius;
    public int damageExplosion=5;
    public AudioClip soundExplosion;
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere (transform.position, radius);
        EnemyManager.Instance.DamageEnemiesInRange(transform.position,radius,damageExplosion);
        if (Vector3.Distance(PlayerManager.Instance.player.transform.position,transform.position)<radius)
            PlayerManager.Instance.playerDegats(radius-(int)(Vector3.Distance(PlayerManager.Instance.player.transform.position,transform.position)));
        
        if (SurvivorManager.Instance.survivor!=null)
        {
            if (Vector3.Distance(SurvivorManager.Instance.survivor.transform.position,transform.position)<radius)
                SurvivorManager.Instance.SurvivorDegats(radius-(int)(Vector3.Distance(SurvivorManager.Instance.survivor.transform.position,transform.position)));
        }
        GameObject[] barrels = GameObject.FindGameObjectsWithTag("baril");
        float minDist = radius;
        GameObject barel=null;
        foreach (GameObject bar in barrels)
        {
            if (Vector3.Distance(transform.position, bar.transform.position) < radius && transform.position!=bar.transform.position)
            {
                if (minDist > Vector3.Distance(transform.position, bar.transform.position))
                {
                    minDist = Vector3.Distance(transform.position, bar.transform.position);
                    barel = bar;
                }
                
                
            }
        }

        if (minDist < radius)
        {
            TimeManager.Instance.AddTimedAction(new TimedAction(() =>
            {
                GameObject explosion1= Instantiate(gameObject, barel.transform.position, barel.transform.rotation) as GameObject;
                Destroy(explosion1, 2f);
                Destroy(barel);
                GetComponent<AudioSource>().PlayOneShot(soundExplosion);
            }, 1f));
        }
        foreach (Collider hit  in colliders) {
            /*if (hit.gameObject.tag == "ennemi")
            {
                EnemyManager.Instance.DamageEnemie(hit.gameObject,-(int)(Vector3.Distance(hit.gameObject.transform.position,transform.position)+radius));
                Debug.Log("ennemi enter");
            }
            if (hit.gameObject.tag == "Player")
            {
                PlayerManager.Instance.playerDegats(-(int)(Vector3.Distance(PlayerManager.Instance.player.transform.position,transform.position)+radius));
            }*/
            
            if (hit.GetComponent<Rigidbody>())
               hit.GetComponent<Rigidbody>().AddExplosionForce (Force, transform.position, radius, 3.0f);
        }
        GameObject explosion= Instantiate(explosionPrefabs, transform.position, transform.rotation) as GameObject;
        Destroy(explosion, 2f);
    }

    /*{
        if (other.gameObject.tag == "ennemi")
        {
            EnemyManager.Instance.DamageEnemie(other.gameObject,(int)(Vector3.Distance(other.gameObject.transform.position,transform.position)*2.5f));
            //Destroy(other.gameObject,10f);
            Debug.Log("ennemi enter");
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerDegats((int)(Vector3.Distance(PlayerManager.Instance.player.transform.position,transform.position)*2.5f));
        }
        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().AddExplosionForce (Force, transform.position, transform.localScale.x, 3.0f);

    }*/
}
