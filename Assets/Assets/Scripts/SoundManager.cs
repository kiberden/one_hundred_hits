using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static AudioClip playerHitSound, playerAttackSound, playerHealSound, playerJumpSound, playerPickupSound;
	static AudioSource audioSrc;

	// Use this for initialization
	void Start () {

		playerHitSound = Resources.Load<AudioClip> ("playerHit");
		playerAttackSound = Resources.Load<AudioClip> ("playerAttack");
		playerHealSound = Resources.Load<AudioClip> ("playerHeal");
		playerJumpSound = Resources.Load<AudioClip> ("playerJump");
		playerPickupSound = Resources.Load<AudioClip> ("playerPickup");

		audioSrc = GetComponent<AudioSource> ();
	
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound (string clip)
	{
		switch (clip) {
		case "playerHit":
			audioSrc.PlayOneShot (playerHitSound);
			break;

		case "playerAttack":
			audioSrc.PlayOneShot (playerAttackSound);
			break;

		case "playerHeal":
			audioSrc.PlayOneShot (playerHealSound);
			break;

		case "playerJump":
			audioSrc.PlayOneShot (playerJumpSound);
			break;

		case "playerPickup":
			audioSrc.PlayOneShot (playerPickupSound);
			break;

		}
	}
}
