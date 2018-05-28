using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_2 : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;
	public float posY;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	private Vector3 velocity = Vector3.zero;

	void Update () {
		Vector3 targetPosition = target.TransformPoint(new Vector3(1, posY, -5));
		Vector3 desiredPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		transform.position = new Vector3 (Mathf.Clamp (desiredPosition.x, minX, maxX), desiredPosition.y,desiredPosition.z); 
	}
}
