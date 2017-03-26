using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Camera : MonoBehaviour {

	public float minFov = 20f;
	public float maxFov = 85f;
	public float sensitivity = 10f;
	public Camera Cam;

	// Update is called once per frame
	void Update () {
		//print ("Cosas");

		float fov = Cam.fieldOfView;

		fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);

		Cam.fieldOfView = fov;
		//Cam.orthographicSize = fov;

		transform.position += new Vector3(Input.GetAxis ("Horizontal") * sensitivity, 0f, Input.GetAxis ("Vertical") * sensitivity);
		//transform.Translate(new Vector3 (Input.GetAxis ("Horizonatal") * speed, 0f, Input.GetAxis ("Vertical") * speed));
		//transform.Translate = new Vector3 (Input.GetAxis ("Horizonatal") * speed, 0f, Input.GetAxis ("Vertical") * speed);
	}
}
