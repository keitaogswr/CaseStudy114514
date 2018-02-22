using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour {

    // 状態
    protected enum STATE
    {
        START,          // 初期位置は移動
        NORMAL,         // 通常時
        DIFFUSION,      // MP4拡散
        NONE
    };

    protected Vector3 nextPoint;              // エネミーが最初に移動する座標
    protected STATE state;                    // エネミーの状態
    EnemyPattern enemyPattern;                // エネミーの行動パターン
    EnemyBulletPattern bulletPattern;         // エネミーの攻撃パターン

    [SerializeField]
    protected float startMoveTime = 2.0f;     // 初期位置に移動するまでの時間

    // Use this for initialization
    void Start()
    {
        state = STATE.START;
        enemyPattern = new EnemyPatternStart();
        enemyPattern.Init(this);
        bulletPattern = new EnemyBulletPatternNone();
    }

    // Update is called once per frame
    void Update()
    {
        enemyPattern.Update(this);
        bulletPattern.Update(this);
    }

    public void ChangeEnemyPattern(EnemyPattern pattern)
    {
        enemyPattern = pattern;
    }

    public void ChangeEnemyBulletPattern(EnemyBulletPattern pattern)
    {
        bulletPattern = pattern;
    }

    public Vector3 NextPoint { get{ return nextPoint; } set { nextPoint = value; } }
    public float StartMoveTime { get { return startMoveTime; } set { startMoveTime = value; } }
    public EnemyPattern EnemyPattern { get { return enemyPattern; } set { enemyPattern = value; } }
    public EnemyBulletPattern BulletPattern { get { return bulletPattern; } set { bulletPattern = value; } }
}
