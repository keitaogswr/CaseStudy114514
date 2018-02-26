using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBossController : EnemyController
{
    [SerializeField]
    private float bossStartMoveTime = 4.0f;
    [SerializeField]
    private GameObject darkAura;

    // Use this for initialization
    void Start ()
    {
        startMoveTime = bossStartMoveTime;
        nextPoint = new Vector3(0, 5, 0);
        enemyPattern = new EnemyBossPatternStart();
        enemyPattern.Init(this);
        bulletPattern = new EnemyBulletPatternNone();
        Instantiate(darkAura, transform);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
