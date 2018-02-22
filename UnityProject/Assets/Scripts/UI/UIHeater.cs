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
	public float		m_AnimTime;		// アニメーション時間
	public float		m_TimeToMax;	// ゲージが最大になるまでの時間

	// private変数
	private float		m_Value;		// ヒートゲージ
	private bool		m_MaxFrag;		// フラグ：ヒートゲージ最大
	private bool		m_ChargeFrag;	// フラグ：再チャージフラグ

	//=========================================================================
	// 初期化処理
	//=========================================================================
	void Start () {
		
		// 人権初期化
		m_Value = 1.0f;
		m_GaugeImage.fillAmount = m_Value;

		// フラグ：オン
		m_MaxFrag = true;
		m_ChargeFrag = false;
		
	}

	//=========================================================================
	// 更新処理
	//=========================================================================
	void FixedUpdate () {
		
		// チャージ可能
		if(m_ChargeFrag)
		{
			// チャージ
			m_Value += 1.0f / (m_TimeToMax * 60);
			m_GaugeImage.fillAmount = m_Value;

			// 最大値になった場合
			if(m_Value >= 1)
			{
				m_Value = 1;
				m_MaxFrag = true;
				m_ChargeFrag = false;
			}
		}
	}

	//=========================================================================
	// 増減処理
	//=========================================================================
	public void AddValue(float inAdd)
	{
		// 加算
		m_Value += inAdd;

		// 例外処理
		if(m_Value >= 1)
		{
			m_Value = 1;
			m_MaxFrag = true;
		}
		else if(m_Value <= 0)
		{
			m_Value = 0;
			m_MaxFrag = false;
		}

		// アニメーション：ゲージ増減
		DOTween.To(
			() => m_GaugeImage.fillAmount,
			Temp => m_GaugeImage.fillAmount = Temp,
			m_Value,								// 最終的な値
			m_AnimTime								// アニメーション時間
		).OnComplete(
			()=>
			{
				// チャージ可能に
				if(m_GaugeImage.fillAmount <= 0) m_ChargeFrag = true;
			}
		);

	}

	//=========================================================================
	// フラグ取得処理
	//=========================================================================
	public bool GetMaxFrag()
	{
		return m_MaxFrag;
	}
}
