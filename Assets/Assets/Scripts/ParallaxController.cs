using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScale;
    public float smoothing;

    private Transform cam;
    private Vector3 previousCamPos;

    // Use this for initialization
    void Start ()
    {
        cam = Camera.main.transform;

        previousCamPos = cam.position;

        parallaxScale = new float[backgrounds.Length];
        for (int i = 0; i < parallaxScale.Length; i++) {
            parallaxScale[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++) {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScale[i];

            float backgroundsTargetPosX = (backgrounds[i].position.x + parallax);

            Vector3 backgroundsTargetPos = new Vector3(backgroundsTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundsTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;

    }
}
