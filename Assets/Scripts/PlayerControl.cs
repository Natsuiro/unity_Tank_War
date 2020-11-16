using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 角色属性
    public float moveSpeed = 1;
    private float timeVal = 0;
    private float defendedTime = 3.0f;
    private bool isDefended;
    // 脚本引用
    private SpriteRenderer sr;
    public Sprite[] sprites;// 上 右 下 左
    public GameObject bulletPrefab;
    public GameObject expPrefab;// 爆炸特效
    public GameObject defendPrefab;

    private Vector3 bulletAngle;
    private Vector3 leftToward = new Vector3(0,0,90);
    private Vector3 rightToward = new Vector3(0,0,-90);
    private Vector3 downToward = new Vector3(0,0,180);
    private Vector3 upToward = new Vector3(0,0,0);
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isDefended = true;
        defendPrefab.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        // 无敌状态控制
        if (isDefended)
        {
            defendedTime -= Time.deltaTime;
            isDefended = defendedTime > 0;
        }else
            defendPrefab.SetActive(isDefended);
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timeVal >= 0.5f)
            {
                DoAttack();
                timeVal = 0;
            } 
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
        // get horizontal axis value
        float dH = Input.GetAxisRaw("Horizontal");
        // get vertical axis value
        float dV = Input.GetAxisRaw("Vertical");
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
        //无敌状态
        if (isDefended)
        {
            return;
        }
        //产生爆炸特效
        Instantiate(expPrefab,transform.position,transform.rotation);
        PlayerManager.Instance.isDead = true;
        //死亡，销毁自己
        Destroy(gameObject);
    }
}
