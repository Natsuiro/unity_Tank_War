using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartControl : MonoBehaviour
{
    private bool hasDead;
    private SpriteRenderer sr;
    public Sprite breakSpr;
    public GameObject expPrefab;
    // Start is called before the first frame update
    void Start()
    {
        hasDead = false;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Broken()
    {
        if (!hasDead)
        {
            hasDead = true;
            sr.sprite = breakSpr;
            Instantiate(expPrefab,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
