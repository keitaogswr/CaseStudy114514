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
	public GameObject	m_GaugeObj;		// オブジェクト：ゲージ
	public float		m_AnimTime;		// アニメーション時間
	public float		m_TimeToMax;	// ゲージが最大になるまでの時間

	// private変数
	private float		m_Value;		// ヒートゲージ
	private Image		m_GaugeImage;	// 画像：ゲージ
	private bool		m_MaxFrag;		// フラグ：ヒートゲージ最大

	//=========================================================================
	// 初期化処理
	//=========================================================================
	void Start () {
		
		// コンポーネント取得：画像
		m_GaugeImage = m_GaugeObj.GetComponent<Image>();

		//// 人権初期化
		//m_Value = 1.0f;
		//m_GaugeImage.fillAmount = m_Value;

		//// フラグ：オン
		//m_MaxFrag = true;
	}
	
	//=========================================================================
	// 更新処理
	//=========================================================================
	void FixedUpdate () {
		
		// ヒートゲージが最大でない場合
		if(!m_MaxFrag)
		{
			m_Value += 1.0f / ( m_TimeToMax * 60 );
			m_GaugeImage.fillAmount = m_Value;

			if(m_Value >= 1)
			{
				m_Value = 1;
				m_MaxFrag = true;
			}
		}
	}

	//=========================================================================
	// 増減処理
	//=========================================================================
	void AddValue(float inAdd)
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
				if(m_Value <= 0) m_MaxFrag = false;
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
