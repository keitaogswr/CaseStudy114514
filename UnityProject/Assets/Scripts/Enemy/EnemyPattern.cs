using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// エネミーの行動パターン
public class EnemyPattern
{
    public virtual void Init(EnemyController enemy) { }             // 初期化
    public virtual void Update(EnemyController enemy) { }           // 更新
    public virtual void ChangePattern(EnemyController enemy) { }    // 状態遷移
}

// エネミーが画面外から初期位置まで移動する
public class EnemyPatternStart : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {
        // 初期位置に移動
        enemy.transform.DOMove(enemy.NextPoint, enemy.StartMoveTime).OnComplete(() => { ChangePattern(enemy); });
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.ChangeEnemyPattern(new EnemyPatternNormal());
        enemy.EnemyPattern.Init(enemy);
    }
}

// エネミーが初期位置に到達したら通常の動きをする
public class EnemyPatternNormal : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {
        // 反復運動
        enemy.transform.DOLocalMoveX(-2, 1).SetRelative().SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public override void Update(EnemyController enemy)
    {
        
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.ChangeEnemyPattern(new EnemyPatternDiffusion());
        enemy.EnemyPattern.Init(enemy);
        enemy.ChangeEnemyBulletPattern(new EnemyBulletPatternFirst());
    }
}

// エネミーがMP4弾を受けたらMP4拡散状態に
public class EnemyPatternDiffusion : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {
        enemy.GetComponent<RectTransform>().DOLocalMoveX(-2, 1);
    }

    public override void Update(EnemyController enemy)
    {
        
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.ChangeEnemyPattern(new EnemyPatternNormal());
        enemy.EnemyPattern.Init(enemy);
    }
}
