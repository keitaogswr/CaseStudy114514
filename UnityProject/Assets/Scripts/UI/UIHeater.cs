//=========================================================================
// 役割		：UIヒートゲージ処理
// 製作者	：野村俊太郎
//=========================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//=========================================================================
// クラス：ONHヒートゲージ
//=========================================================================
public class UIHeater : MonoBehaviour {

	// public変数
	public Image		m_GaugeImage;	// 画像：ゲージ

	// private変数
	private Player		m_Player;		// プレイヤー

	//=========================================================================
	// 初期化処理
	//=========================================================================
	void Start () {
		
		// ゲージ初期化
		m_GaugeImage.fillAmount = 1.0f;
		
		// プレイヤー探索
		m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	//=========================================================================
	// 更新処理
	//=========================================================================
	void FixedUpdate () {
		m_GaugeImage.fillAmount = m_Player.GetHeat();
	}
}
