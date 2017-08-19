using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    int i = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        i++;
        Vector3 pos =this.gameObject.transform.localPosition;
        if (i % 3 == 0)
        {
            pos.x = 0;
        }
        else
        {
            pos.x = pos.x + 1;
        }

        this.gameObject.transform.localPosition = pos;

    }
}
