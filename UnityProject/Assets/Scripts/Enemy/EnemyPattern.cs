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
    public virtual void Destroy(EnemyController enemy) { }          // 破壊
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

    public override void Update(EnemyController enemy)
    {
        // 画面外になったら消滅
        if (enemy.transform.position.x < -20 || enemy.transform.position.x > 20)
        {
            Object.Destroy(enemy.gameObject);
        }
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.ChangeEnemyPattern(new EnemyPatternDiffusion());
        enemy.ChangeEnemyBulletPattern(new EnemyBulletPatternNormal());
        enemy.EnemyPattern.Init(enemy);
        enemy.ChangeSpriteDiffusion();
    }

    public override void Destroy(EnemyController enemy)
    {
        Object.Instantiate(enemy.Explosion, new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0), Quaternion.identity);
        SoundManager.PlaySE(6, 0.2f);
        Object.Destroy(enemy.gameObject);
    }
}

// エネミーがMP4弾を受けたらMP4拡散状態に
public class EnemyPatternDiffusion : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {

        GameObject obj = Object.Instantiate(enemy.Metamorphose, enemy.transform);
        SoundManager.PlaySE(5, 0.2f);

        enemy.Player.AddDignity(-0.05f);
    }

    public override void Update(EnemyController enemy)
    {
        // 画面外になったら消滅
        if (enemy.transform.position.x < -20 || enemy.transform.position.x > 20)
        {
            Object.Destroy(enemy.gameObject);
        }
    }

    public override void ChangePattern(EnemyController enemy)
    {
        //enemy.ChangeEnemyPattern(new EnemyPatternNormal());
        //enemy.EnemyPattern.Init(enemy);
    }

    public override void Destroy(EnemyController enemy)
    {
        Object.Instantiate(enemy.Explosion, new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0), Quaternion.identity);
        SoundManager.PlaySE(6, 0.2f);
        Object.Destroy(enemy.gameObject);
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
        enemy.ChangeEnemyBulletPattern(new EnemyBulletPatternFirst());
        enemy.EnemyPattern.Init(enemy);
    }
}

// ボスの拡散行動
public class EnemyBossPatternDiffusion : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {
        // 反復運動
        //Sequence seq = DOTween.Sequence();
        //seq.Append(enemy.transform.DOLocalMoveX(-2, 0.5f).SetRelative().SetEase(Ease.Linear));
        //seq.Append(enemy.transform.DOLocalMoveX(4, 1).SetRelative().SetEase(Ease.Linear));
        //seq.Append(enemy.transform.DOLocalMoveX(-2, 0.5f).SetRelative().SetEase(Ease.Linear));
        //seq.SetLoops(-1);
    }

    public override void Update(EnemyController enemy)
    {

    }

    public override void ChangePattern(EnemyController enemy)
    {
    }
}

// 拡散オブジェの初期移動パターン
public class EnemyDiffusionerPatternStart : EnemyPattern
{
    public override void Init(EnemyController enemy)
    {
        // 初期位置に移動
        enemy.transform.DOMove(enemy.NextPoint, enemy.StartMoveTime).OnComplete(() => { ChangePattern(enemy); });
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.ChangeEnemyPattern(new EnemyDiffusionerPatternDiffusion());
        enemy.ChangeEnemyBulletPattern(new EnemyBulletPatternFirst());
        enemy.EnemyPattern.Init(enemy);
    }
}

// 拡散オブジェの拡散行動
public class EnemyDiffusionerPatternDiffusion : EnemyPattern
{
    private float nextPatternTime = 15;
    public override void Init(EnemyController enemy)
    {
        // 反復運動
        Sequence seq = DOTween.Sequence();
        seq.Append(enemy.transform.DOLocalMoveX(-2, 0.5f).SetRelative().SetEase(Ease.Linear));
        seq.Append(enemy.transform.DOLocalMoveX(4, 1).SetRelative().SetEase(Ease.Linear));
        seq.Append(enemy.transform.DOLocalMoveX(-2, 0.5f).SetRelative().SetEase(Ease.Linear));
        seq.SetLoops(-1);
    }

    public override void Update(EnemyController enemy)
    {
        enemy.NextPatternTime += Time.deltaTime;

        if(enemy.NextPatternTime > nextPatternTime)
        {
            enemy.NextPatternTime = 0;
            ChangePattern(enemy);
        }
    }

    public override void ChangePattern(EnemyController enemy)
    {
        enemy.ChangeEnemyPattern(new EnemyDiffusionerPatternEnd());
        enemy.ChangeEnemyBulletPattern(new EnemyBulletPatternNone());
        enemy.EnemyPattern.Init(enemy);
    }
}

// 拡散オブジェの拡散行動
public class EnemyDiffusionerPatternEnd : EnemyPattern
{
    private float nextPatternTime = 15;
    public override void Init(EnemyController enemy)
    {
        // 初期位置に移動
        enemy.transform.DOMove(new Vector3(0, 15, 0), enemy.StartMoveTime).OnComplete(() => { ChangePattern(enemy); });
    }

    public override void ChangePattern(EnemyController enemy)
    {
        Object.Destroy(enemy.gameObject);
    }
}