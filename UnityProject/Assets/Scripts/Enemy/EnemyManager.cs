using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネミーの生成
public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private int enemyNum = 4;                                       // 敵の数
    [SerializeField]
    private EnemyController enemyObject;                            // 敵のプレハブ
    private List<Vector3> positionList = new List<Vector3>();       // 敵の生成位置リスト
    private List<Vector3> nextPointList = new List<Vector3>();      // 敵の初期位置リスト

	// Use this for initialization
	void Start () {
        // 生成位置リストの初期化
        positionList.Add(new Vector3(15.0f, 0.0f, 0.0f));
        positionList.Add(new Vector3(15.0f, -3.0f, 0.0f));
        positionList.Add(new Vector3(-15.0f, 0.0f, 0.0f));
        positionList.Add(new Vector3(-15.0f, -3.0f, 0.0f));

        // 初期位置リストの初期化
        nextPointList.Add(new Vector3(10.0f, 0.0f, 0.0f));
        nextPointList.Add(new Vector3(5.0f, -3.0f, 0.0f));
        nextPointList.Add(new Vector3(-8.0f, 0.0f, 0.0f));
        nextPointList.Add(new Vector3(-3.0f, -3.0f, 0.0f));

        for (int i = 0; i < enemyNum; i++)
        {
            EnemyController enemy = Instantiate(enemyObject);
            enemy.transform.position = positionList[i];
            enemy.NextPoint = nextPointList[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
