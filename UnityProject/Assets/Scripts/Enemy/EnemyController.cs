using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour {
    
    protected Vector3 nextPoint;                        // エネミーが最初に移動する座標
    protected EnemyPattern enemyPattern;                // エネミーの行動パターン
    protected EnemyBulletPattern bulletPattern;         // エネミーの攻撃パターン

    [SerializeField]
    protected float startMoveTime = 2.0f;               // 初期位置に移動するまでの時間

    protected float nextPatternTime = 0;
    [SerializeField]
    protected Bullet bullet;                            // 弾
    protected float bulletTime;                         // 弾の発射までの残り時間
    [SerializeField]
    protected float bulletInterval = 3.0f;              // 弾の発射間隔

    private SpriteRenderer MainSpriteRenderer;          // 使用スプライト
    [SerializeField]
    protected Sprite normalSprite;                      // 通常時テクスチャ
    [SerializeField]
    protected Sprite diffusionSprite;                   // 拡散時テクスチャ
    private Player player;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject metamorphose;

    // Use this for initialization
    void Start()
    {
        // このobjectのSpriteRendererを取得
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        MainSpriteRenderer.sprite = normalSprite;
        enemyPattern = new EnemyPatternStart();
        enemyPattern.Init(this);
        bulletPattern = new EnemyBulletPatternNone();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        enemyPattern.Update(this);
        bulletPattern.Update(this);
    }

    // エネミーの行動パターン変更
    public void ChangeEnemyPattern(EnemyPattern pattern)
    {
        enemyPattern = pattern;
    }

    // エネミーの攻撃パターン変更
    public void ChangeEnemyBulletPattern(EnemyBulletPattern pattern)
    {
        bulletPattern = pattern;
    }

    // エネミーの当たり判定
    void OnTriggerEnter2D(Collider2D collider)
    {
        string layerName = LayerMask.LayerToName(collider.gameObject.layer);
        // 衝突：MP4弾
        if (layerName == "MP4Bullet")
        {
            // 行動変化
            enemyPattern.ChangePattern(this);
            player.AddDignity(-0.05f);
        }
        // 衝突：プレイヤー弾
        if(layerName == "PlayerBullet")
        {
            // 破壊
            enemyPattern.Destroy(this);
        }
    }

    public void ChangeSpriteDiffusion()
    {
        MainSpriteRenderer.sprite = diffusionSprite;
    }

    public Vector3 NextPoint { get{ return nextPoint; } set { nextPoint = value; } }
    public float StartMoveTime { get { return startMoveTime; } set { startMoveTime = value; } }
    public EnemyPattern EnemyPattern { get { return enemyPattern; } set { enemyPattern = value; } }
    public EnemyBulletPattern BulletPattern { get { return bulletPattern; } set { bulletPattern = value; } }
    public Bullet Bullet { get { return bullet; } set { bullet = value; } }
    public float BulletTime { get { return bulletTime; } set { bulletTime = value; } }
    public float BulletInterval { get { return bulletInterval; } set { bulletInterval = value; } }
    public Sprite DiffusionSprite { get { return DiffusionSprite; } set { DiffusionSprite = value; } }
    public float NextPatternTime { get { return nextPatternTime; } set { nextPatternTime = value; } }
    public GameObject Explosion { get { return explosion; } }
    public GameObject Metamorphose { get { return metamorphose; } }
}
