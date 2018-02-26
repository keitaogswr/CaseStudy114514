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
        enemy.BulletTime += Time.deltaTime;

        if(enemy.BulletTime >= enemy.BulletInterval)
        {
            enemy.BulletTime = 0;
            // 弾の発射
            Bullet bullet = Object.Instantiate(enemy.Bullet);
            bullet.Shoot(new Vector2(0, -1), enemy.gameObject);
            bullet = Object.Instantiate(enemy.Bullet);
            bullet.Shoot(new Vector2(0.75f, -0.75f), enemy.gameObject);
            bullet = Object.Instantiate(enemy.Bullet);
            bullet.Shoot(new Vector2(-0.75f, -0.75f), enemy.gameObject);
        }
    }
}