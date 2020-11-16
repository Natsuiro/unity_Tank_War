using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // 角色属性
    private Vector3 bornPos;
    public float moveSpeed = 1;
    private float timeVal = 0;
    private float moveTimer = 0;
    float dH = 0;
    float dV = 0;
    float attackTime;
    // 脚本引用
    private SpriteRenderer sr;
    public Sprite[] sprites;// 上 右 下 左
    public GameObject bulletPrefab;
    public GameObject expPrefab;// 爆炸特效

    private Vector3 bulletAngle;
    private Vector3 leftToward = new Vector3(0, 0, 90);
    private Vector3 rightToward = new Vector3(0, 0, -90);
    private Vector3 downToward = new Vector3(0, 0, 180);
    private Vector3 upToward = new Vector3(0, 0, 0);

    public void SetBornPos(Vector3 pos )
    {
        bornPos = pos;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveTimer = Random.Range(0, 3);
        attackTime = Random.Range(2, 5);
        dH = Random.Range(-1, 2);
        dV = Random.Range(-1, 2);
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (timeVal >= attackTime)
        {
            DoAttack();
            timeVal = 0;
        }
        timeVal += Time.deltaTime;
    }
    private void DoAttack()
    {
        Instantiate(bulletPrefab,
            transform.position,
            Quaternion.Euler(transform.eulerAngles + bulletAngle));
    }
    private void FixedUpdate()
    {
        if (moveTimer >= 2)
        {
            // get horizontal axis value
            dH = Random.Range(-1, 2);
            // get vertical axis value
            dV = Random.Range(-1, 2);
            moveTimer = 0;
        }
        else moveTimer += Time.fixedDeltaTime;
        // do action
        DoMove(dH, dV);
        DoRotate(dH, dV);
    }
    private void DoMove(float x, float y)
    {

        if (y != 0)
            transform.Translate(transform.up * y * Time.fixedDeltaTime * moveSpeed, Space.World);
        else
            transform.Translate(transform.right * x * Time.fixedDeltaTime * moveSpeed, Space.World);
    }

    private void DoRotate(float x, float y)
    {
        if (x < 0)
        {
            sr.sprite = sprites[3];
            bulletAngle = leftToward;
        }
        else if (x > 0)
        {
            sr.sprite = sprites[1];
            bulletAngle = rightToward;
        }

        if (y < 0)
        {
            sr.sprite = sprites[2];
            bulletAngle = downToward;
        }
        else if (y > 0)
        {
            sr.sprite = sprites[0];
            bulletAngle = upToward;
        }
    }

    public void Die()
    {
        // 重生一个坦克
        PlayerManager.Instance.ReBornEnemyTank();
        //产生爆炸特效
        Instantiate(expPrefab, transform.position, transform.rotation);
        //死亡，销毁自己
        Destroy(gameObject);

    }
}
