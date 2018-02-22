﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

	// Use this for initialization
	void Start () {
        moveTime = 0.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        moveTime += Time.deltaTime;//時間加算

        //一定時間たったら戻る
        if(moveTime >= life)
        {
            Destroy(gameObject);
        }
	}
}
