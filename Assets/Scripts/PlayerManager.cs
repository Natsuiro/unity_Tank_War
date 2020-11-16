using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    private static PlayerManager instance;

    public List<Vector3> locList;

    public bool isDead;
    private bool isDefeat;
    public GameObject bornPrefab;
    public GameObject bg;
    private int lifeValue;
    public int playerScore;
    public static PlayerManager Instance {
        get => instance;
    }
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bg.SetActive(false);
        isDefeat = false;
        isDead = false;
        lifeValue = 3;
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            if (!bg.activeSelf)
            {
                bg.SetActive(true);
                Invoke("ReturnToMain",3);
            }
            return;
        }
        if (isDead)
        {
            Recover();
        }
    }

    private void Recover()
    {
        if (lifeValue <= 0)
        {
            Debug.Log("Game Over");
            isDefeat = true;
            // game over
        }
        else{
            lifeValue--;
            //reBorn
            GameObject born = Instantiate(bornPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            born.GetComponent<BornControl>().BornPlayer();
            // reFlag
            isDead = false;
        }
    }

    public void ReBornEnemyTank()
    {
        Vector3 pos = locList[Random.Range(0,locList.Count)];
        GameObject born = Instantiate(bornPrefab, pos, Quaternion.identity);
        born.GetComponent<BornControl>().BornEnemy();
    }

    private void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

}
