using UnityEngine;
using System.Collections;

public class AnimArme1 : MonoBehaviour {

    private Animator Anim;
    public Animator AnimFlamme;

    public Animator AnimFlamme2; 
    //public bool Target { get; private set; } = true;
    void Start () {
        Anim = GetComponent<Animator>();
	}
	
	
	void Update () {
	
        //fire
        if(Input.GetButton("Fire1"))
        {
            if (!Anim.GetBool("target"))
            {
                Anim.SetTrigger("fire");
                if(GetComponent<Tir>().cartouches > 0)
                {
                    AnimFlamme.SetBool("flamme", true);
                }
                else
                {
                    AnimFlamme.SetBool("flamme", false);
                }    
            }
            else
            {
                if(GetComponent<Tir>().cartouches > 0)
                {
                    AnimFlamme2.SetBool("flamme", true);
                }
                else
                {
                    AnimFlamme2.SetBool("flamme", false);
                }    
            }

                    
           
        }        

        if(Input.GetButtonUp("Fire1"))
        {
            AnimFlamme.SetBool("flamme", false);
            AnimFlamme2.SetBool("flamme", false);
        }

        //reload
        if (!Anim.GetBool("target"))
        {
            if (Input.GetKeyDown(KeyCode.R) && GetComponent<Tir>().cartouches == 0 && GetComponent<Tir>().chargeurs> 0)
            {
                Anim.SetTrigger("reload");
            }
        }
        
        //walk
        if (Input.GetAxis("Vertical")!=0 && !Input.GetKey(KeyCode.LeftShift))
        {
            Anim.SetBool("walk", true);
        }else
        {
            Anim.SetBool("walk", false);
        }

        //run
        if (Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            Anim.SetBool("run", true);
        }
        else
        {
            Anim.SetBool("run", false);
        }

        //target enemy
        if (Input.GetKey(KeyCode.Tab))
        {
            Anim.SetBool("target",true);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Anim.SetBool("target",false);
        }
        

    }
}
