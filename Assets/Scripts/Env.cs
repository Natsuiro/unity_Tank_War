using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env : MonoBehaviour
{
    // map Size 20 * 16
    public GameObject[] items;
    private List<Vector3> locList;
    private int heartNum = 0;
    private int bornNum = 0;
    private int Born = 0;
    private int Barriar = 1;
    private int Grass = 2;
    private int Heart = 3;
    private int River = 4;
    private int Wall = 5;
    private int AirWall = 6;
 

    private void Awake()
    {
        locList = new List<Vector3>();
        instantiateEnv();
        PlayerManager.Instance.locList = locList;
    }
    private void instantiateEnv()
    {
        for (int x = -10;x<=10;x++)
        {
            CreateItem(items[AirWall], new Vector3(x, 9, 0));
            CreateItem(items[AirWall], new Vector3(x, -9, 0));
        }
        for (int y = -8;y<=9;y++)
        {
            CreateItem(items[AirWall], new Vector3(-11, y, 0));
            CreateItem(items[AirWall], new Vector3(11, y, 0));
        }
        for (int i = -10;i<=10;i++)
        {
            for (int j = -8;j<=8;j++)
            {
                if (i+j == 0)
                {
                    continue;
                }
                int num = Random.Range(0,10);// [0,5]
                switch (num)
                {
                    case 0:
                        if (bornNum >= 10) break;
                        Vector3 pos = new Vector3(i, j, 0);
                        locList.Add(pos);
                        GameObject item = CreateItem(items[Born], pos);
                        item.GetComponent<BornControl>().BornEnemy();
                        bornNum++;
                        break;
                    case 1:
                        CreateItem(items[Barriar], new Vector3(i, j, 0));
                        break;
                    case 2:
                        CreateItem(items[Grass], new Vector3(i, j, 0));
                        break;
                    case 3://Heart
                        if (heartNum >= 5) break;
                        heartNum++;
                        CreateItem(items[Heart], new Vector3(i, j, 0));
                        break;
                    case 4:
                        CreateItem(items[River], new Vector3(i, j, 0));
                        break;
                    case 5:
                        CreateItem(items[Wall], new Vector3(i, j, 0));
                        break;
                    default:
                        break;
                }
            }
        }

        
        GameObject ctl = CreateItem(items[Born], new Vector3(0, 0, 0));
        ctl.GetComponent<BornControl>().BornPlayer();
    }
    private GameObject CreateItem(GameObject item,Vector3 pos)
    {
        GameObject newItem = Instantiate(item,pos,Quaternion.identity);
        newItem.transform.SetParent(gameObject.transform);
        return newItem;
    }
}
