using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalControl instance;
    public float hp = 100;
    public float fp = 100;
    
    public GameObject Player;
    void Awake()
    {
        Player = FindObjectOfType<PlayerManager>().gameObject;
        Application.targetFrameRate = 144;
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
