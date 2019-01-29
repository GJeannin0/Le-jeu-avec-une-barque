using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPaddleAnim : MonoBehaviour
{
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetButtonDown("PaddleUpRight"))
		{
			anim.SetTrigger("PaddleBackRight");
		}

		if (Input.GetButtonDown("PaddleBackRight"))
		{
			anim.SetTrigger("PaddleUpRight");
		}
	}
}
