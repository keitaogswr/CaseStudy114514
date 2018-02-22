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

// エネミーが生成位置から反対側まで移動する
public class EnemyPatternStart : EnemyPattern
{
    Rigidbody2D rigidbody;

    public override void Init(EnemyController enemy)
    {
        rigidbody = enemy.GetComponent<Rigidbody2D>();

        if (enemy.transform.position.x > 0) rigidbody.velocity = new Vector2(-2, 0);
        if (enemy.transform.position.x < 0) rigidbody.velocity = new Vector2(2, 0);
    }

    public override void ChangePattern(EnemyController enemy)
    {
        //enemy.ChangeEnemyPattern(new EnemyPatternNormal());
        enemy.EnemyPattern.Init(enemy);
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
        //enemy.ChangeEnemyPattern(new EnemyPatternNormal());
        enemy.EnemyPattern.Init(enemy);
    }
}

// ボスの初期移動パターン
public class EnemyBossPatternStart : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {
        // 初期位置に移動
        enemy.transform.DOMove(enemy.NextPoint, enemy.StartMoveTime).OnComplete(() => { ChangePattern(enemy); });
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.ChangeEnemyPattern(new EnemyBossPatternDiffusion());
        enemy.EnemyPattern.Init(enemy);
    }
}

// ボスの拡散行動
public class EnemyBossPatternDiffusion : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {
        // 反復運動
        Sequence seq = DOTween.Sequence();
        seq.Append(enemy.transform.DOLocalMoveX(-2, 0.5f).SetRelative().SetEase(Ease.Linear));
        seq.Append(enemy.transform.DOLocalMoveX(4, 1).SetRelative().SetEase(Ease.Linear));
        seq.Append(enemy.transform.DOLocalMoveX(-2, 0.5f).SetRelative().SetEase(Ease.Linear));
        seq.SetLoops(-1);
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.EnemyPattern.Init(enemy);
    }
}