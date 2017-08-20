using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceParticle : MonoBehaviour {

    public GameObject[] particle;

    private int counter = 0;

	// Use this for initialization
	void Start () {

        Object.Instantiate(particle[0]);

    }
	
	// Update is called once per frame
	void Update () {

        counter++;
        if (counter > 500)
        {
            counter = 0;
            Object.Instantiate(particle[0]);
        }

        if (counter == 500)
            Object.Instantiate(particle[1]);

	}
}