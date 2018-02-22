using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public Rigidbody2D rigid2D;    //剛体
    public float power;            //スピード調整用変数
    public float life;             //弾が発射されてから戻るまでの秒数

    float moveTime = 0.0f;         //弾が発射されてから戻るまでの秒数を計測する変数
    GameObject parent;             //弾を発射したオブジェクト

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

    //弾を発射する。
    public void Shoot(GameObject obj = null)
    {
        parent = obj;

        moveTime = 0.0f;

        gameObject.transform.position = parent.transform.position;

        rigid2D.AddForce(parent.transform.up*power);
    }
}
