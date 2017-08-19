using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger: MonoBehaviour {

    float speed = 0.03f;
    float angle;
    float radius = 0.02f;

    // Use this for initialization
    void Start()
    {

        angle = 0;

    }

    // Update is called once per frame
    void Update()
    {

        angle += speed;
        if (speed >= 360)
            speed = 0;

        float scaleChanger = radius * (float)Mathf.Sin(angle);

        transform.localScale = Vector3.one * scaleChanger + Vector3 .one;

    }
}
