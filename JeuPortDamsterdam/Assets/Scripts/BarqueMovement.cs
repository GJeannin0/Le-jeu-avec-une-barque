using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BarqueMovement : MonoBehaviour
{

	[SerializeField] private float turnSpeed = 1.0f;

	private float rotationSpeed = 0.0f;
	[SerializeField] private float angularDrag = 0.5f;

	[SerializeField] private Vector2 accelerationDirection = new Vector3(1.0f,3.0f);
	[SerializeField] private float accelerationSpeed = 10.0f;

	private Rigidbody2D myRigidbody;
	private Vector2 newVelocity = new Vector2(0.0f, 0.0f);

	private bool paddledUpLeft = false;
	private float paddledUpLeftTimer = 0.0f;

	private bool paddledUpRight = false;
	private float paddledUpRightTimer = 0.0f;

	private bool paddledDownLeft = false;
	private float paddledDownLeftTimer = 0.0f;

	private bool paddledDownRight = false;
	private float paddledDownRightTimer = 0.0f;

	[SerializeField] private float maxTimeBetweenSyncroPaddles = 0.1f;
	[SerializeField] private float bonusSpeedWhenSyncro = 1.0f;



    void Start()
    {
		myRigidbody = GetComponent<Rigidbody2D>();
	}


    void Update()
    {
		if (paddledDownLeft)
		{
			if (paddledDownLeftTimer < maxTimeBetweenSyncroPaddles)
			{
				paddledDownLeftTimer += Time.deltaTime;
			}
			else
			{
				paddledDownLeftTimer = 0.0f;
				paddledDownLeft = false;
			}
		}

		if (paddledUpRight)
		{
			if (paddledUpRightTimer < maxTimeBetweenSyncroPaddles)
			{
				paddledUpRightTimer += Time.deltaTime;
			}
			else
			{
				paddledUpRightTimer = 0.0f;
				paddledUpRight= false;
			}
		}

		if (paddledUpLeft)
		{
			if (paddledUpLeftTimer < maxTimeBetweenSyncroPaddles)
			{
				paddledUpLeftTimer += Time.deltaTime;
			}
			else
			{
				paddledUpLeftTimer = 0.0f;
				paddledUpLeft = false;
			}
		}

		if (paddledDownRight)
		{
			if (paddledDownRightTimer < maxTimeBetweenSyncroPaddles)
			{
				paddledDownRightTimer += Time.deltaTime;
			}
			else
			{
				paddledDownRightTimer = 0.0f;
				paddledDownRight = false;
			}
		}

		if (paddledDownLeft && paddledDownRight)
		{
			paddledDownLeft = false;
			paddledDownRight = false;
			paddledDownLeftTimer = 0.0f;
			paddledDownRightTimer = 0.0f;

			myRigidbody.velocity += new Vector2(-transform.up.x, -transform.up.y) * bonusSpeedWhenSyncro;
		}

		if (paddledUpLeft && paddledUpRight)
		{
			paddledUpRight = false;
			paddledUpLeft = false;
			paddledUpRightTimer = 0.0f;
			paddledUpLeftTimer = 0.0f;

			myRigidbody.velocity += new Vector2(transform.up.x,transform.up.y) * bonusSpeedWhenSyncro;
		}

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
		paddledUpLeft = true;
	}

	private void PaddleUpRight()
	{
		Vector2 localDirection = (transform.right * accelerationDirection.x + transform.up * accelerationDirection.y).normalized;
		myRigidbody.velocity += new Vector2(localDirection.x, localDirection.y) * accelerationSpeed;
		Turn(-1);
		paddledUpRight = true;
	}

	private void PaddleDownLeft()
	{
		Vector2 localDirection = (transform.right * -accelerationDirection.x + transform.up * -accelerationDirection.y).normalized;
		myRigidbody.velocity += new Vector2(localDirection.x, localDirection.y) * accelerationSpeed;
		Turn(-1);
		paddledDownLeft = true;
	}

	private void PaddleDownRight()
	{
		Vector2 localDirection = (transform.right * accelerationDirection.x + -transform.up * accelerationDirection.y).normalized;
		myRigidbody.velocity += new Vector2(localDirection.x, localDirection.y) * accelerationSpeed;
		Turn(1);
		paddledDownRight = true;
	}
}
