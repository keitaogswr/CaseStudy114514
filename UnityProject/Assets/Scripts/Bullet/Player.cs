using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Bullet bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            Bullet obj = Instantiate(bullet);

            obj.Shoot(gameObject.transform.up, gameObject);
        }
	}
}
