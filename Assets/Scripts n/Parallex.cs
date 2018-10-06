using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour {

    public float scrollSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float z = Mathf.Repeat(Time.time * scrollSpeed, 1);
        this.gameObject.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(0f,z));
	}
}
