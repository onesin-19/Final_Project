using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class AnimArme1 : MonoBehaviour {

    private Animator Anim;
    public Animator AnimFlamme;

    public Animator AnimFlamme2;


    private float DepartZoom;
    public float curZoom;
    public float zoom=20;
    //public bool Target { get; private set; } = true;
    void Start () {
        Anim = GetComponent<Animator>();
        DepartZoom = Camera.main.fieldOfView;
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
            if ((Input.GetKeyDown(KeyCode.R)||Input.GetButtonDown("PS4_O")) && GetComponent<Tir>().cartouches == 0 && GetComponent<Tir>().chargeurs> 0)
            {
                Anim.SetTrigger("reload");
            }
        }
        
        //walk
        if (Input.GetAxis("Vertical")!=0 && (!Input.GetKey(KeyCode.LeftShift)&&!Input.GetButton("PS4_R2")))
        {
            Anim.SetBool("walk", true);
        }else
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

        //target enemy
        if (Input.GetButton("Fire2")||(Input.GetButton("PS4_L2")))
        {
            zoomCam();
            Anim.SetBool("target",true);
        }
        else
        {
            Anim.SetBool("target",false);
            Camera.main.fieldOfView = DepartZoom;
            curZoom = DepartZoom;
        }
        /*if (Input.GetButtonUp("Fire2")||(Input.GetButton("PS4_Triangle")&&Input.GetButton("PS4_R2")))
        {
            Anim.SetBool("target",false);
        }*/
        

    }

    void zoomCam()
    {
        curZoom-=1f;
        if (curZoom > zoom)
        {
            Camera.main.fieldOfView = curZoom;
        }
    }
}
