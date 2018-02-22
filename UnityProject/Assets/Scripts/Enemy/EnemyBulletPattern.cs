using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPattern
{
    public virtual void Init(EnemyController enemy) { }             // 初期化
    public virtual void Update(EnemyController enemy) { }           // 更新
    public virtual void ChangePattern(EnemyController enemy) { }    // 状態遷移
}

public class EnemyBulletPatternNone : EnemyBulletPattern
{
    public override void Update(EnemyController enemy)
    {
    }
}

public class EnemyBulletPatternFirst : EnemyBulletPattern
{
    public override void Update(EnemyController enemy)
    {
        // 弾の発射
    }
}