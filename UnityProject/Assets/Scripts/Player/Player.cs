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

	// private変数
	private Rigidbody2D		m_RB;					// リジッドボディー

	private float			m_MoveVertical;			// 縦移動
	private float			m_MoveHorizontal;		// 横移動
	private bool			m_LowMoveFrag;			// フラグ：低速移動

	//=========================================================================
	// 初期化処理
	//=========================================================================
	void Start () {
		
		// コンポーネント取得：リジッドボディ―
		m_RB = GetComponent<Rigidbody2D>();

		// フラグ：オフ
		m_LowMoveFrag = false;
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
	}
}
