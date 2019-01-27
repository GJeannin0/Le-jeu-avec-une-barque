using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BarqueMovement : MonoBehaviour
{

	[SerializeField] private float turnSpeed = 1.0f;

	private float rotationSpeed = 0.0f;
	[SerializeField] private float angularDrag = 0.5f;

	[SerializeField] private Vector2 accelerationDirection = new Vector3(1.0f,4.0f);
	[SerializeField] private float accelerationSpeed = 10.0f;

	private Rigidbody2D myRigidbody;
	private Vector2 newVelocity = new Vector2(0.0f, 0.0f);

    void Start()
    {
		myRigidbody = GetComponent<Rigidbody2D>();
	}


    void Update()
    {
		transform.Rotate(Vector3.back * rotationSpeed);

		if (Math.Abs(rotationSpeed) <= angularDrag * Time.deltaTime)
		{
			rotationSpeed = 0.0f;
		}
		else
		{
			if (rotationSpeed < 0)
			{
				rotationSpeed += angularDrag * Time.deltaTime;
			}
			else
			{
				if (rotationSpeed > 0)
				{
					rotationSpeed -= angularDrag * Time.deltaTime;
				}
			}
		}

		if (Input.GetButtonDown("Fire1"))		// test buttons
		{
			PaddleUpLeft();
		}

		if (Input.GetButtonDown("Fire2"))
		{
			PaddleUpRight();
		}

		if (Input.GetButtonDown("Fire3"))
		{
			PaddleDownLeft();
		}

		if (Input.GetButtonDown("Jump"))
		{
			PaddleDownRight();
		}
	}

	private void Turn(int way) // way -1 to turn right, 1 to turn left
	{
		rotationSpeed += way * turnSpeed;
	}

	private void PaddleUpLeft()
	{
		Vector2 localDirection = (transform.right * accelerationDirection.x + -transform.up * accelerationDirection.y).normalized;
		myRigidbody.velocity += new Vector2(-localDirection.x, -localDirection.y) * accelerationSpeed;
		Turn(1);
	}

	private void PaddleUpRight()
	{
		Vector2 localDirection = (transform.right * accelerationDirection.x + transform.up * accelerationDirection.y).normalized;
		myRigidbody.velocity += new Vector2(localDirection.x, localDirection.y) * accelerationSpeed;
		Turn(-1);
	}

	private void PaddleDownLeft()
	{
		Vector2 localDirection = (transform.right * -accelerationDirection.x + transform.up * -accelerationDirection.y).normalized;
		myRigidbody.velocity += new Vector2(localDirection.x, localDirection.y) * accelerationSpeed;
		Turn(-1);
	}

	private void PaddleDownRight()
	{
		Vector2 localDirection = (transform.right * accelerationDirection.x + -transform.up * accelerationDirection.y).normalized;
		myRigidbody.velocity += new Vector2(localDirection.x, localDirection.y) * accelerationSpeed;
		Turn(1);
	}
}
