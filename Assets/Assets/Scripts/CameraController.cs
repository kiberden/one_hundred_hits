using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;

	Vector3 velocity = Vector3.zero;

	public float smoothtime = .15f;

	public bool YMaxEnabled = false;
	public float YMaxValue = 15;

	public bool YMinEnabled = false;
	public float YMinValue = 0;

	public bool XMaxEnabled = false;
	public float XMaxValue = 0;

	public bool XMinEnabled = false;
	public float XMinValue = 0;

    public float offsetY = 10f;
    public float TARGRT_Y = 0f;
    public float MIN_Y = 0f;
    public float RESULT_Y = 0f;

    void FixedUpdate()
	{
		Vector3 targetPos = target.position;

        /*debug*/
        this.RESULT_Y = Mathf.Clamp(10f, -3f, -1f);

        if (YMinEnabled && YMaxEnabled) {
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
        } else if (YMinEnabled) {
             targetPos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
            /**/this.TARGRT_Y = target.position.y;
            /**/this.MIN_Y = YMinValue;
            /**/this.RESULT_Y = targetPos.y;
        } else if (YMaxEnabled) {
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);
        }


		if (XMinEnabled && XMaxEnabled)
			targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);

		else if (YMinEnabled)
			targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);

		else if (XMaxEnabled)
			targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);


		targetPos.z = transform.position.z;

		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothtime);
	}
}
