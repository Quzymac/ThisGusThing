using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTest : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales; // The proportion of the camera's movement to move the backgrounds by. :)
    public float smoothness = 1f; // Makes things smoooooth. Always above 0!

    private Transform myCamera;
    private Vector3 previousCameraPos;


    private void Awake() 
    {
        myCamera = Camera.main.transform;
    }

    void Start() {
        previousCameraPos = myCamera.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; // Brackeys made me do it.
        }
	}

    void Update() {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCameraPos.x - myCamera.position.x) * parallaxScales[i];

            float backgroundTargetPositionx = backgrounds[i].position.x + parallax; // The target position for the background[i].

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPositionx, backgrounds[i].position.y, backgrounds[i].position.z);

            //time to fade shit (current and target pos) with lerp

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothness * Time.deltaTime);                  
        }

        previousCameraPos = myCamera.position;
        
	}
}
