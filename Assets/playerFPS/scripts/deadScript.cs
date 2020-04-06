using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadScript : MonoBehaviour {

    Animator anim;
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	
	public void PlayerAnimDead () {
        anim.enabled = true;		
	}
}
