using UnityEngine;
using System.Collections;

public class AnimArme2 : MonoBehaviour {

    private Animator Anim;
    public Animator AnimFlamme;

    void Start()
    {
        Anim = GetComponent<Animator>();
    }


    void Update()
    {

        //fire
        if (Input.GetButtonDown("Fire1") && GetComponent<Tir>().cartouches > 0 && GetComponent<Tir>().canFire)
        {
            Anim.SetTrigger("fire");

            if (GetComponent<Tir>().cartouches > 0)
            {
                AnimFlamme.SetTrigger("flamme");
            }
           
        }

        //reload
        if ((Input.GetKeyDown(KeyCode.R)||Input.GetButtonDown("PS4_O")) && GetComponent<Tir>().cartouches == 0 && GetComponent<Tir>().chargeurs > 0)
        {
            Anim.SetTrigger("reload");
        }

        //walk
        if (Input.GetAxis("Vertical") != 0 && (!Input.GetKeyDown(KeyCode.LeftShift)&&!Input.GetButton("PS4_R2")))
        {
            Anim.SetBool("walk", true);
        }
        else
        {
            Anim.SetBool("walk", false);
        }

        //run
        if (Input.GetAxis("Vertical") != 0 && (Input.GetKey(KeyCode.LeftShift)||Input.GetButton("PS4_R2")))
        {
            Anim.SetBool("run", true);
        }
        else
        {
            Anim.SetBool("run", false);
        }


    }
}
