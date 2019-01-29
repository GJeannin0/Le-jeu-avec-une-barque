using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPaddleAnim : MonoBehaviour
{
	private Animator anim;
	
    void Start()
    {
		anim = GetComponent<Animator>();
    }

    void Update()
    {
		if (Input.GetButtonDown("PaddleUpLeft"))
		{
			anim.SetTrigger("PaddleBackLeft");
		}

		if (Input.GetButtonDown("PaddleBackLeft"))
		{
			anim.SetTrigger("PaddleUpLeft");
		}
	}
}
