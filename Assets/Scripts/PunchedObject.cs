using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PunchedObject : MonoBehaviour {
    public Action<Collision> OnHit;

    void OnCollisionEnter(Collision collision){
        if(OnHit != null){
            OnHit(collision);
        }
	}
}
