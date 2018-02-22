//=========================================================================
// 役割		：UI人権ゲージ処理
// 製作者	：野村俊太郎
//=========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//=========================================================================
// クラス：人権ゲージ
//=========================================================================
public class UIDignity : MonoBehaviour {

	// public変数
	public Image		m_GaugeImage;	// 画像：ゲージ

	// private変数
	private Player		m_Player;		// プレイヤー

	//=========================================================================
	// 初期化処理
	//=========================================================================
	void Start () {

		// ゲージ
		m_GaugeImage.fillAmount = 1.0f;

		// プレイヤー探索
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	//=========================================================================
	// 更新処理
	//=========================================================================
	void FixedUpdate () {

		// プレイヤーの体力参照
		m_GaugeImage.fillAmount = m_Player.GetDignity();
		
		// カラー更新
		ChangeColor();
	}

	//=========================================================================
	// 色変更処理
	//=========================================================================
	void ChangeColor()
	{
		float Temp = m_GaugeImage.fillAmount;

		if(0.0f <= Temp && Temp < 0.25f)
		{
			m_GaugeImage.color = Color.red;
		}
		else if(0.25f <= Temp && Temp < 0.5f)
		{
			m_GaugeImage.color = Color.yellow;
		}
		else if(0.5f <= Temp && Temp <= 1.0f)
		{
			m_GaugeImage.color = Color.green;
		}
	}
}
