using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khornit : MonoBehaviour {

	public int dir = 1;
	public float cur_dist = 0;
	public float max_dist = 1;
	public float speed = 0.0005f;
	public bool Damage_Khornit = true;
	Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		cur_dist += speed;
		{
			cur_dist = 0;
			dir *= -1;
			transform.localScale = new Vector3 (dir, transform.localScale.y, transform.localScale.z);
		}
		
		if (dir == 1) {
			transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.y);
		} else {
			transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.y);
		}

	}
		public void Death ()
		{
			if (Damage_Khornit)
			{
				anim.SetBool ("Damage_Khornit", false);
				speed = 0;

			}
		}

}
