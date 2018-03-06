﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GroundTiltControl : MonoBehaviour {

    public float tiltAngle;

    private Vector2 planeTilt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TiltControl();
	}

    void TiltControl() {
        Vector2 tilt = new Vector2(Input.GetAxis("Horizontal") * tiltAngle, Input.GetAxis("Vertical") * tiltAngle);

        planeTilt.y = Mathf.Clamp(tilt.y, -tiltAngle, tiltAngle);
        planeTilt.x = Mathf.Clamp(tilt.x, -tiltAngle, tiltAngle);

        Quaternion xRot = Quaternion.AngleAxis(planeTilt.x, Vector3.back);
        Quaternion yRot = Quaternion.AngleAxis(planeTilt.y, Vector3.right);

        Quaternion rotation = new Quaternion(xRot.x + yRot.x, xRot.y + yRot.y, xRot.z + yRot.z, xRot.w + yRot.w);

        transform.rotation = rotation;
    }
}
