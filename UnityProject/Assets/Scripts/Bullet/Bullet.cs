using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Rigidbody2D rigid2D;//剛体
	public float power;            //スピード調整用変数
	public float life;             //弾が発射されてから戻るまでの秒数

	protected float moveTime = 0.0f;         //弾が発射されてから戻るまでの秒数を計測する変数
	protected Vector2 moveVec;             //弾を発射したオブジェクト
    [SerializeField]
    protected ParticleSystem effect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//弾を発射する。
	public void Shoot(Vector2 vec, GameObject obj = null)
	{
		moveVec = vec;

		moveTime = 0.0f;

		gameObject.transform.position = obj.transform.position;

		rigid2D.AddForce(moveVec * power);
	}
}
