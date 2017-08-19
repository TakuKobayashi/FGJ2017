using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateMotion : MonoBehaviour {

    float speed = 0.04f;
    float angle;

	// Use this for initialization
	void Start () {

        angle = 0;

	}
	
	// Update is called once per frame
	void Update () {

        angle += speed;
        if (speed >= 360)
            speed = 0;

        transform.Translate(new Vector3(0, 0.002f * (float)Mathf.Sin(angle),0));

	}
}
