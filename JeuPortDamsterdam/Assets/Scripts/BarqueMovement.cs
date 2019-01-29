using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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

	[SerializeField] private GameObject cameraHolder;
	[SerializeField] private float cameraMaximumDistanceFromPlayer = 2.0f;

	[SerializeField] private float rotationInvertRatio = -0.7f;

	[SerializeField] private float baseLife = 40.0f;
	private float currentLife = 1.0f;

	[SerializeField] private float invincibilityDuration = 2.0f;
	private float invincibilityTimer = 0.0f;
	private bool isInvincible = false;

	private SpriteRenderer mySpriteRenderer;
	[SerializeField] private Color baseColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	[SerializeField] private Color invincibilityColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);

	void Start()
    {
		myRigidbody = GetComponent<Rigidbody2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		currentLife = baseLife;
	}


    void Update()
    {
		if (isInvincible)
		{
			if (mySpriteRenderer.color == baseColor)
			{
				mySpriteRenderer.color = invincibilityColor;
			}
			else
			{
				mySpriteRenderer.color = baseColor;
			}
				invincibilityTimer += Time.deltaTime;
			if (invincibilityTimer > invincibilityDuration)
			{
				invincibilityTimer = 0.0f;
				isInvincible = false;
				mySpriteRenderer.color = baseColor;
			}
		}

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

		if (Input.GetButtonDown("PaddleUpLeft"))	
		{
			PaddleUpLeft();
		}

		if (Input.GetButtonDown("PaddleUpRight"))
		{
			PaddleUpRight();
		}

		if (Input.GetButtonDown("PaddleBackLeft"))
		{
			PaddleDownLeft();
		}

		if (Input.GetButtonDown("PaddleBackRight"))
		{
			PaddleDownRight();
		}

		cameraHolder.transform.position = transform.position + transform.up + new Vector3 (myRigidbody.velocity.x, myRigidbody.velocity.y);

		if ((transform.position - cameraHolder.transform.position).magnitude > cameraMaximumDistanceFromPlayer)
		{
			cameraHolder.transform.position = transform.position + transform.up + (new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y)).normalized * cameraMaximumDistanceFromPlayer;
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == ("Map Border"))
		{
			rotationSpeed *= rotationInvertRatio;
		}

		if (!isInvincible)
		{
			if (collision.gameObject.tag == ("End"))
			{
				SceneManager.LoadScene("Guillaume");
			}

			currentLife--;
			if (currentLife <= 0.0f)
			{
				Debug.Log("Perdu");
				SceneManager.LoadScene("Guillaume");
			}
			else
			{
				isInvincible = true;
			}
		}
	}
}
