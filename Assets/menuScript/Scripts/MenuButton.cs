using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	private Button b;

	private void Start()
	{
		b = GetComponent<Button>();
	}

	// Update is called once per frame
    void Update()
    {
		if(EventSystem.current.currentSelectedGameObject==gameObject)
		{
			animator.SetBool ("selected", true);
			if(Input.GetButtonDown("Submit")){
				animator.SetBool ("pressed", true);
				b?.onClick.Invoke();
			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool ("selected", false);
		}
    }
}
