using UnityEngine;
using System.Collections;

public class Tir : MonoBehaviour {

    public AudioClip SoundShoot, SoundReload, SoundEmpty,soundExplosion;
    private Ray ray;
    private RaycastHit hit;
    public float shootRate=1f;
    public float nextFire;
    public int cartouches, chargeurs, max_cartouches;
    public GameObject BulletHolePrefab, SparksPrefab;
    public bool Automatic = true, canFire=false;
    public GameObject explosionZone;
    void Start()
    {
        //PanelUI = GameObject.Find("PanelUI");
        //PanelUI.GetComponent<UiScript>().UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
        UIManager.Instance.UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
    }

    void OnEnable()
    {
        //UIManager.Instance.UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
        /*if(PanelUI)
        {
            PanelUI.GetComponent<UiScript>().UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
        }*/
       
    }


	void Update () {

        if(Time.time> nextFire)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }
        
//        UIManager.Instance.UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);

        switch (Automatic)
        {
            case true: //si Automatique
                //Tir
                if (Input.GetButton("Fire1") && cartouches > 0)
                {
                    if (Time.time > nextFire)
                    {
                        cartouches -= 1;
                        UIManager.Instance.UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
                        //PanelUI.GetComponent<UiScript>().UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);

                        if (GetComponent<Animator>().GetBool("target"))
                        {
                            shootRate = 0.5f;
                        }
                        else
                        {
                            shootRate = 0.08f;
                        }
                        nextFire = Time.time + shootRate;

                        GetComponent<AudioSource>().PlayOneShot(SoundShoot);
                        Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
                        ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);

                        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
                        {
                            if (hit.transform.gameObject.tag == "ennemi")
                            {
                                EnemyManager.Instance.DamageEnemie(hit.transform.gameObject,PlayerStats.damage);
                                //hit.transform.gameObject.GetComponent<dead>().ennemiDead();
                            }

                            if (hit.transform.gameObject.tag == "decor")
                            {
                                //bullet hole
                                GameObject Impact;
                                Impact = Instantiate(BulletHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                                Destroy(Impact, 60f);
                                //sparks
                                GameObject Sparks;
                                Sparks = Instantiate(SparksPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                                Destroy(Sparks, 3f);
                            }
                            if (hit.transform.gameObject.tag == "baril")
                            {
                                GameObject explosion= Instantiate(explosionZone, hit.transform.position, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                                Destroy(explosion, 2f);
                                Destroy(hit .transform.gameObject);
                                GetComponent<AudioSource>().PlayOneShot(soundExplosion);
                            }
                        }
                    }
                }
                break;

            case false: //pas Automatique
                //Tir
                if (Input.GetButtonDown("Fire1") && cartouches > 0)
                {
                    if (Time.time > nextFire)
                    {
                    
                        cartouches -= 1;
                        UIManager.Instance.UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);

                        nextFire = Time.time + shootRate;

                        GetComponent<AudioSource>().PlayOneShot(SoundShoot);
                        Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
                        ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);

                        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
                        {
                            if (hit.transform.gameObject.tag == "ennemi")
                            {
                                EnemyManager.Instance.DamageEnemie(hit.transform.gameObject,PlayerStats.damage);
                            }

                            if (hit.transform.gameObject.tag == "decor")
                            {
                                //bullet hole
                                GameObject Impact;
                                Impact = Instantiate(BulletHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                                Destroy(Impact, 60f);
                                //sparks
                                GameObject Sparks;
                                Sparks = Instantiate(SparksPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                                Destroy(Sparks, 3f);
                            }
                            if (hit.transform.gameObject.tag == "baril")
                            {
                                GameObject explosion= Instantiate(explosionZone, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                                Destroy(explosion, 2f);
                                Destroy(hit .transform.gameObject);
                                GetComponent<AudioSource>().PlayOneShot(soundExplosion);
                            }
                        }
                    }
                }
                break;

        }
        
        //Recharge
        if (!GetComponent<Animator>().GetBool("target"))
        {
            if((Input.GetKeyDown(KeyCode.R)||Input.GetButtonDown("PS4_O")) && cartouches==0 && chargeurs>0)
            {           
                GetComponent<AudioSource>().PlayOneShot(SoundReload);
                StartCoroutine(Recharge());
            }
        }
        
        //Plus de cartouches

        if (Input.GetButton("Fire1") && cartouches == 0 && Time.time > nextFire)
        {
            GetComponent<AudioSource>().PlayOneShot(SoundEmpty);
            nextFire = Time.time + shootRate;
        } 
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(0.2f);
        chargeurs -= 1;
        cartouches += max_cartouches;
        UIManager.Instance.UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
        //PanelUI.GetComponent<UiScript>().UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
    }

    public void AddChargeurs(int nb)
    {
        chargeurs += nb;
        UIManager.Instance.UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
        //PanelUI.GetComponent<UiScript>().UpdateTxtCartouches(cartouches, max_cartouches, chargeurs);
    }
    
}
