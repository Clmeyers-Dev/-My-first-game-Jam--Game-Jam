using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRoverBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject target;
    public Rigidbody2D rb;
    public bool playerRight;
    public float maxTime;
    public float currentTime;
    public float distance;
    public float range;
    public bool triggered;
    void Start()
    {
        
        target = FindObjectOfType<PlayerManager>().gameObject;
        
        checkForPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position,target.transform.position);
        if(distance<range){
            triggered = true;
        }
        if(triggered){
     if(currentTime>0){
        dash();
         currentTime-=Time.deltaTime;
     }else{
         checkForPlayer();
     } 
    }  
    }
    public void dash(){
        
        if(playerRight==true){
        rb.velocity = new Vector2(1*speed,rb.velocity.y);
        }else{
           rb.velocity = new Vector2(-1*speed,rb.velocity.y); 
        }
    }
    public void checkForPlayer(){
        if(target.transform.position.x>transform.position.x){
            playerRight = true;
              transform.eulerAngles = new Vector3 (0,180,0);
        }else{
            playerRight =false;
           
             transform.eulerAngles = new Vector3 (0,0,0);
        }
        currentTime = maxTime;
    }
}
