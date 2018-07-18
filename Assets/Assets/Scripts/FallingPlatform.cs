using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
    private Animator animFall;

	public float fallDelay;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
       animFall = GetComponent<Animator>();
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag("Player")) 
		{

            animFall.SetTrigger("falling");
            StartCoroutine(Fall());
                        
        }
	}

	IEnumerator Fall()
	{
		yield return new WaitForSeconds(fallDelay);
        animFall.enabled = false;
        rb2d.isKinematic = false;
        
        yield return 0;
}
}
