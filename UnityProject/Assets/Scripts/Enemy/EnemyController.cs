using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour {
    
    protected Vector3 nextPoint;                        // エネミーが最初に移動する座標
    protected EnemyPattern enemyPattern;                // エネミーの行動パターン
    protected EnemyBulletPattern bulletPattern;         // エネミーの攻撃パターン

    [SerializeField]
    public float startMoveTime = 2.0f;               // 初期位置に移動するまでの時間

    // Use this for initialization
    void Start()
    {
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

    // エネミーの行動パターン変更
    public void ChangeEnemyPattern(EnemyPattern pattern)
    {
        enemyPattern = pattern;
    }

    // エネミーの攻撃パターン変更
    public void ChangeEnemyBulletPattern(EnemyBulletPattern pattern)
    {
        bulletPattern = pattern;
    }

    public Vector3 NextPoint { get{ return nextPoint; } set { nextPoint = value; } }
    public float StartMoveTime { get { return startMoveTime; } set { startMoveTime = value; } }
    public EnemyPattern EnemyPattern { get { return enemyPattern; } set { enemyPattern = value; } }
    public EnemyBulletPattern BulletPattern { get { return bulletPattern; } set { bulletPattern = value; } }
}
