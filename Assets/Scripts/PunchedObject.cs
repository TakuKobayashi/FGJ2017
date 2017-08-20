using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchedObject : MonoBehaviour {
    void OnCollisionEnter(Collision collision){
        Debug.LogFormat("Enter:{0}", collision.gameObject.name);
	}

	void OnCollisionExit(Collision collision){
		Debug.LogFormat("Exit:{0}", collision.gameObject.name);
	}
}
