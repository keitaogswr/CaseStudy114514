using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mp4Bullet : Bullet {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        moveTime += Time.deltaTime;//時間加算

        //一定時間たったら戻る
        if (moveTime >= life)
        {
            Destroy(gameObject);
        }
    }
}
