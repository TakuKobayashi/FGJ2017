using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObjectManager : MonoBehaviour {

    public float changeHeight;

    public GameObject playerObject;
    public GameObject [] backObject;

	// Use this for initialization
	void Start () {

        backObject[0].SetActive(true);
        backObject[1].SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

        if (playerObject.transform.position.y > changeHeight)
        {
            backObject[0].SetActive(false);
            backObject[1].SetActive(true);
        }
        else
        {
            backObject[0].SetActive(true);
            backObject[1].SetActive(false);
        }
	}
}
