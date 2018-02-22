using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    public Renderer render;
    [Range(0, 1)]
    public float speed;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float scroll = Mathf.Repeat(Time.time* speed, 1);
        Vector2 offset = new Vector2(0, scroll);
        render.sharedMaterial.SetTextureOffset("_MainTex",offset);
	}
}
