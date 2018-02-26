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
    private Player player;

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

        if(time >= intervalTime)
        {
            time = 0;
            NormalEnemyCreate();
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
