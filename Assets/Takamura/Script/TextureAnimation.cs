using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimation: MonoBehaviour {

    public Texture[] texture;
    private Texture currentTexture;

    public Material material;

    private int counter = 0;

    private int offset = 0;
    private Random random;

	// Use this for initialization
	void Start () {

        material.SetTexture("_MainTex", texture[0]);

	}
	
	// Update is called once per frame
	void Update () {

        counter++;
        if (counter >200)
            counter = 0;

        offset = Random.Range(0, 2);

        if (counter < 90 + offset
            || (counter > 100 + offset && counter < 110 + offset) 
            || (counter > 140 + offset && counter < 150 + offset) 
            || counter > 160 + offset)
            material.SetTexture("_MainTex", texture[0]);
        else
            material.SetTexture("_MainTex", texture[1]);
	}
}
