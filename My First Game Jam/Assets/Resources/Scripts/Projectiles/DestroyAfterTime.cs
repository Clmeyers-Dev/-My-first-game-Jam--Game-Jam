using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public bool automatic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(automatic)
        Destroy(gameObject,time);
    }
    public void destoryObject(float timeToKill){
        Destroy(gameObject,timeToKill);
    }
}
