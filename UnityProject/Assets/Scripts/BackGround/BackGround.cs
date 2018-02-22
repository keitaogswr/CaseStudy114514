using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    public Renderer render;
    [Range(0, 1)]
    public float speed;

    bool isMove = false;

	// Use this for initialization
	void Start () {
        isMove = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(!isMove)
        {
            return;
        }
        float scroll = Mathf.Repeat(Time.time* speed, 1);
        Vector2 offset = new Vector2(0, scroll);
        render.sharedMaterial.SetTextureOffset("_MainTex",offset);
	}

    public void MoveStop()
    {
        isMove = false;
    }

    public void MoveStart()
    {
        isMove = true;
    }
}
