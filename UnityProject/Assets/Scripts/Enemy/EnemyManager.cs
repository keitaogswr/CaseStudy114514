using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネミーの生成
public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private int enemyNum = 4;                                       // 敵の生成数
    [SerializeField]
    private EnemyController enemyObject;                            // 敵のプレハブ
    private List<Vector3> positionList = new List<Vector3>();       // 敵の生成位置リスト

    private float time = 0;
    [SerializeField]
    private int intervalTime = 5;
    [SerializeField]
    private GameObject enemyBoss;
    private float bossTime;
    private bool bCreateBoss = true;
    [SerializeField]
    private BackGround backGround;
    private bool bCreate = true;

    // Use this for initialization
    void Start () {
        // 生成位置リストの初期化
        positionList.Add(new Vector3(18.0f, -1.0f, 0.0f));
        positionList.Add(new Vector3(15.0f, -5.0f, 0.0f));
        positionList.Add(new Vector3(-18.0f, -1.0f, 0.0f));
        positionList.Add(new Vector3(-15.0f, -5.0f, 0.0f));

        NormalEnemyCreate();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        bossTime += Time.deltaTime;

        if (time >= intervalTime && bCreate)
        {
            time = 0;
            NormalEnemyCreate();
        }

        if(bossTime >= 19)
        {
            backGround.GoLastBattle();
        }

        if(bossTime >= 20 && bCreateBoss)
        {
            bCreateBoss = false;
            Instantiate(enemyBoss);
        }

        if(bossTime >= 40)
        {
            bCreate = false;
        }
	}

    void NormalEnemyCreate()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            EnemyController enemy = Instantiate(enemyObject);
            enemy.transform.position = positionList[i];
        }
    }
}
