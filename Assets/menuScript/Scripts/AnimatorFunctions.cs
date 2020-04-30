using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	public bool disableOnce;

	void PlaySound(AudioClip whichSound){
		if(!disableOnce){
			audioSource.PlayOneShot (whichSound);
		}else{
			disableOnce = false;
		}
	}
}	
