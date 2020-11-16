using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pointer : MonoBehaviour
{
    private int option = 1;
    public Transform optionOne;
    public Transform optionTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            option = 1;
            transform.position = optionOne.position;
        }else if (Input.GetKeyDown(KeyCode.DownArrow)){
            option = 2;
            transform.position = optionTwo.position;
        }else if (Input.GetKeyDown(KeyCode.Space)){
            if (option == 1)
            {
                SceneManager.LoadScene(1);  
            }
        }
    }
}
