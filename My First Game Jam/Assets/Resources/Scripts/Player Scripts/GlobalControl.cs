using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalControl instance;
    public float hp = 100;
    public float fp = 100;
    void Awake()
    {
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
