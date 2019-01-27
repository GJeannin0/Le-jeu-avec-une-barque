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
		Vector2 localDirection = (transform.right * accelerationDirection.x + -transform.up * accelerationDirection.y).normalized;

		Debug.DrawLine(transform.position, transform.position + new Vector3(-localDirection.x, -localDirection.y, 0.0f) * accelerationSpeed, Color.red, 10.0f, false);
		Debug.DrawLine(transform.position, transform.position + new Vector3(-localDirection.x, -localDirection.y, 0.0f) * accelerationSpeed, Color.blue, 10.0f, false);
		Debug.DrawLine(transform.position, transform.position + new Vector3(localDirection.x, localDirection.y, 0.0f) * accelerationSpeed, Color.yellow, 10.0f, false);
		Debug.DrawLine(transform.position, transform.position + new Vector3(localDirection.x, localDirection.y, 0.0f) * accelerationSpeed, Color.green, 10.0f, false);

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

		if (Input.GetButtonDown("Fire1"))
		{
			Paddle(1, 1);
		}

		if (Input.GetButtonDown("Fire2"))
		{
			Paddle(1, -1);
		}

		if (Input.GetButtonDown("Fire3"))
		{
			Paddle(-1, 1);
		}

		if (Input.GetButtonDown("Jump"))
		{
			Paddle(-1, -1);
		}
	}

	private void Turn(int way) // way -1 to turn right, 1 to turn left
	{
		rotationSpeed += way * turnSpeed;
	}

	private void Paddle(int side, int way) // side -1 from left, 1 from right     way -1 forward, 1 backward
	{
		Vector2 localDirection = (transform.right * accelerationDirection.x + transform.up * accelerationDirection.y).normalized;

		if (side == -1)																	// -- +-		where you paddle
		{																				// -+ ++
			if (way == -1)
			{
				myRigidbody.velocity += new Vector2(-localDirection.x, -localDirection.y) * accelerationSpeed;
				Turn(-1);
				
			}
			else
			{
				if (way == 1)
				{
					myRigidbody.velocity += new Vector2(-localDirection.x, localDirection.y) * accelerationSpeed;
					Turn(1);
					
				}
			}
		}
		else
		{
			if (side == 1)
			{
				if (way == -1)
				{
					myRigidbody.velocity += new Vector2(localDirection.x, -localDirection.y) * accelerationSpeed;
					Turn(1);
					
				}
				else
				{
					if (way == 1)
					{
						myRigidbody.velocity += new Vector2(localDirection.x, localDirection.y) * accelerationSpeed;
						Turn(1);
						
					}
				}
			}
		}
	}
}
