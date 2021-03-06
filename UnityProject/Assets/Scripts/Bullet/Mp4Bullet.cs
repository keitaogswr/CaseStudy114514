﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mp4Bullet : Bullet {
    [SerializeField]
    private float noColisionTime = 1.0f; // 弾が発射されてから当たり判定がない時間
    private float collisionTime;
    private Collider2D colliderMp4;

	// Use this for initialization
	void Start () {
        colliderMp4 = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        moveTime += Time.deltaTime;//時間加算
        collisionTime += Time.deltaTime;

        //一定時間たったら戻る
        if (moveTime >= life)
        {
            EffectDestroy();
            Destroy(gameObject);
        }

        if(collisionTime >= noColisionTime)
        {
            colliderMp4.enabled = true;
        }
    }

    // エネミーの当たり判定
    void OnTriggerEnter2D(Collider2D collider)
    {
        string layerName = LayerMask.LayerToName(collider.gameObject.layer);
        // 衝突：敵
        if (layerName == "Enemy")
        {
            EffectDestroy();
            Destroy(gameObject);
            Debug.Log("MP4Bullet:Enemy Hit");
        }

        // 衝突：プレイヤー
        if (layerName == "Player")
        {
            EffectDestroy();
            Destroy(gameObject);
            Debug.Log("MP4Bullet:Player Hit");
        }
    }

    private void EffectDestroy()
    {
        effect.Stop();
        gameObject.transform.DetachChildren();
    }
}
