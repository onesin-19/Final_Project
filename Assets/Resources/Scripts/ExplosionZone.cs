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
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere (transform.position, radius);
        EnemyManager.Instance.DamageEnemiesInRange(transform.position,radius,damageExplosion);
        if (Vector3.Distance(PlayerManager.Instance.player.transform.position,transform.position)<radius)
            PlayerManager.Instance.playerDegats(-(int)(Vector3.Distance(PlayerManager.Instance.player.transform.position,transform.position)+radius));
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
                hit.GetComponent<Rigidbody>().AddExplosionForce (Force, transform.position, transform.localScale.x, 3.0f);
        }  
        GameObject explosion= Instantiate(explosionPrefabs, transform.position, transform.rotation) as GameObject;
        Destroy(explosion, 1f);
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
