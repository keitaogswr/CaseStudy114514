using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBossController : MonoBehaviour
{
    // 状態
    protected enum STATE
    {
        START,          // 初期位置は移動
        NORMAL,         // 通常時
        DIFFUSION,      // MP4拡散
        NONE
    };

    protected Vector3 nextPoint;  // エネミーが最初に移動する座標
    protected STATE state;          // エネミーの状態
    [SerializeField]
    protected float startMoveTime = 2.0f;
    EnemyPattern enemyPattern;

    // Use this for initialization
    void Start () {
        state = STATE.START;
        nextPoint = new Vector3(0, 7, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.DOMove(nextPoint, startMoveTime);
	}
}
