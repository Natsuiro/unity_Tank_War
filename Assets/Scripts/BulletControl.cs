using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float moveSpeed;
    public bool isPlayerBullet;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime,Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    PlayerManager.Instance.playerScore++;
                    Debug.Log("Score:"+PlayerManager.Instance.playerScore);
                    Destroy(gameObject);
                }
                break;
            case "Barriar":
                Destroy(gameObject);
                break;
            case "Heart":
                collision.SendMessage("Broken");
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
