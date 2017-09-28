using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour
{
	public float maxSpeed = 10f;
	public float move;

	void Start()
	{

	}

	void Update()
	{
		Move();
	}

	private static void Move()
	{
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
	}
}