using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornControl : MonoBehaviour
{
    public bool createPlayer;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        createPlayer = false;
        
    }
    public void BornPlayer()
    {
        Invoke("BornPlayerLater", 1f);
    }

    public void BornEnemy()
    {
        Invoke("BornEnemyLater", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornPlayerLater()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void BornEnemyLater()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyControl>().SetBornPos(transform.position);
        Destroy(gameObject);
    }
}
