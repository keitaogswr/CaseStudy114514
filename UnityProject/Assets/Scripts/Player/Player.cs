//=========================================================================
// 役割		：プレイヤー処理
// 製作者	：野村俊太郎
//=========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//=========================================================================
// クラス：プレイヤー
//=========================================================================
public class Player : MonoBehaviour {

	// 定数
	[SerializeField]
	private float SPEED;
	[SerializeField]
	private float LOW_SPEED;

	// public変数
	public Bullet			m_BulletPrefab;			// プレハブ：弾

	// private変数
	[SerializeField]
	private Rigidbody2D		m_RB;					// リジッドボディ：プレイヤー

	private float			m_MoveVertical;			// 縦移動
	private float			m_MoveHorizontal;		// 横移動
	private bool			m_LowMoveFrag;			// フラグ：低速移動
	private float			m_Dignity;				// 人権
	private float			m_Heat;					// ヒート
	private float			m_HeatCastTime;			// ヒートのキャストタイム
	private bool			m_ChargeFrag;			// フラグ：ヒート再チャージフラグ

	//=========================================================================
	// 初期化処理
	//=========================================================================
	void Start () {

		// ヒート
		m_Heat = 1.0f;
		m_HeatCastTime = 1.2f;
		// 人権
		m_Dignity = 1.0f;
		
		// フラグ：オフ
		m_LowMoveFrag = false;
		m_ChargeFrag = false;

		// 仮：必ず解除
		SoundManager.PlayBGM(0, 0.1f);
	}
	
	//=========================================================================
	// 更新処理
	//=========================================================================
	void Update () {

		// 操作更新
		OperateUpdate();

		// 低速切り替え
		float TempSpeed;
		TempSpeed = m_LowMoveFrag ? LOW_SPEED : SPEED;

		// 速度更新
		m_RB.velocity = new Vector2(m_MoveHorizontal * TempSpeed, m_MoveVertical * TempSpeed);

		// ヒートゲージ更新
		HeatUpdate();
	}

	//=========================================================================
	// 操作処理
	//=========================================================================
	void OperateUpdate()
	{
		// 左右移動
		if((Input.GetAxis("Horizontal") < 0) || (Input.GetKey(KeyCode.LeftArrow)))
		{
			// 左
			m_MoveHorizontal = -1;
		}
		else if((Input.GetAxis("Horizontal") > 0) || (Input.GetKey(KeyCode.RightArrow)))
		{
			// 右
			m_MoveHorizontal = 1;
		}
		else
		{
			m_MoveHorizontal = 0;
		}

		// 上下移動
		if((Input.GetAxis("Vertical") > 0) || (Input.GetKey(KeyCode.UpArrow)))
		{
			// 上
			m_MoveVertical = 1;
		}
		else if((Input.GetAxis("Vertical") < 0) || (Input.GetKey(KeyCode.DownArrow)))
		{
			// 下
			m_MoveVertical = -1;
		}
		else
		{
			m_MoveVertical = 0;
		}

		// 低速移動
		if((Input.GetKey(KeyCode.LeftShift)))
		{
			m_LowMoveFrag = true;
		}
		else
		{
			m_LowMoveFrag = false;
		}

		// 弾発射
		if(Input.GetKeyDown(KeyCode.Z))
		{
			// ヒートゲージが溜まっていた場合
			if(m_Heat >= 1.0f)
			{
				Bullet BulletObj = Instantiate(m_BulletPrefab);
				BulletObj.Shoot(new Vector2(0, 1), gameObject);

				// ヒートゲージ初期化
				AddHeat(-1.0f);

				// SE再生：弾発射
				SoundManager.PlaySE(0, 0.5f);
			}
		}
	}

	//=========================================================================
	// ヒートの更新処理
	//=========================================================================
	void HeatUpdate()
	{
		// チャージ可能
		if(m_ChargeFrag)
		{
			// チャージ
			m_Heat += 1.0f / (m_HeatCastTime * 60);

			// 最大値になった場合
			if(m_Heat >= 1)
			{
				m_Heat = 1;
				m_ChargeFrag = false;
			}
		}

	}

	//=========================================================================
	// 衝突処理
	//=========================================================================
	void OnTriggerEnter2D(Collider2D collision)
	{
		// 衝突：MP4弾
		if(collision.gameObject.CompareTag("MP4Bullet"))
		{
			// 人権回復
			AddDignity(0.05f);

			// SE再生：回復
			SoundManager.PlaySE(3);
		}
	}

	//=========================================================================
	// 人権の増減処理
	//=========================================================================
	public void AddDignity(float inAdd)
	{
		// 加算
		float EndDignity = m_Dignity;
		EndDignity += inAdd;

		// 例外処理
		if		(EndDignity >= 1) EndDignity = 1;
		else if	(EndDignity <= 0) EndDignity = 0;

		// アニメーション：ゲージ増減
		DOTween.To(
			() => m_Dignity,
			Temp => m_Dignity = Temp,
			EndDignity,								// 最終的な値
			0.2f									// アニメーション時間
		);
	}

	//=========================================================================
	// ヒートの増減処理
	//=========================================================================
	void AddHeat(float inAdd)
	{
		// 加算
		float EndHeat = m_Heat;
		EndHeat += inAdd;

		// 例外処理
		if		(EndHeat >= 1) EndHeat = 1;
		else if	(EndHeat <= 0) EndHeat = 0;

		// アニメーション：ゲージ増減
		DOTween.To(
			() =>m_Heat,
			Temp => m_Heat = Temp,
			EndHeat,								// 最終的な値
			0.2f									// アニメーション時間
		).OnComplete(
			()=>
			{
				// チャージ可能に
				if(m_Heat <= 0) m_ChargeFrag = true;

				// チャージSE再生
				SoundManager.PlaySE(1);
			}
		);
	}

	//=========================================================================
	// 人権の取得処理
	//=========================================================================
	public float GetDignity()
	{
		return m_Dignity;
	}

	//=========================================================================
	// ヒートの取得処理
	//=========================================================================
	public float GetHeat()
	{
		return m_Heat;
	}

}
