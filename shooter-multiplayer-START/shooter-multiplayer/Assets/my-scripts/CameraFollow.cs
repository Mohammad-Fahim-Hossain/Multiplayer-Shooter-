﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject Target; // the main chracter that the camera follows
	public float cameraSmoothing = 10.0f;

	Vector3 offset; // the distance between the main character and the camera
	// Use this for initialization
	void Start () {
        if (Target != null)
        {
            offset = transform.position - Target.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Target != null)
        {
            Vector3 targetCamera = Target.transform.position + offset;

           
            transform.position = Vector3.Lerp(transform.position, targetCamera, Time.deltaTime * cameraSmoothing);
        }
	}
}
