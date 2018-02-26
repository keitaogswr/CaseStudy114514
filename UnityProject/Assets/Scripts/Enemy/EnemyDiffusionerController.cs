using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiffusionerController : EnemyController {

	// Use this for initialization
	void Start () {
        nextPoint = new Vector3(0, 5, 0);
        enemyPattern = new EnemyDiffusionerPatternStart();
        enemyPattern.Init(this);
        bulletPattern = new EnemyBulletPatternNone();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
