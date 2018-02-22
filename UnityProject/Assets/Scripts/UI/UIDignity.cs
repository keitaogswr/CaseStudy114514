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
	public float		m_AnimTime;		// アニメーション時間

	// private変数
	private float		m_Value;		// 人権量

	//=========================================================================
	// 初期化処理
	//=========================================================================
	void Start () {
		
		// 人権初期化
		m_Value = 1.0f;
		m_GaugeImage.fillAmount = m_Value;
	}
	
	//=========================================================================
	// 更新処理
	//=========================================================================
	void Update () {
		
	}

	//=========================================================================
	// 増減処理
	//=========================================================================
	public void AddValue(float inAdd)
	{
		// 加算
		m_Value += inAdd;

		// 例外処理
		if		(m_Value >= 1) m_Value = 1;
		else if	(m_Value <= 0) m_Value = 0;

		// アニメーション：ゲージ増減
		DOTween.To(
			() => m_GaugeImage.fillAmount,
			Temp => m_GaugeImage.fillAmount = Temp,
			m_Value,								// 最終的な値
			m_AnimTime								// アニメーション時間
		).OnComplete(
			() =>
			{ 
				ChangeColor();
			}
		);

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
