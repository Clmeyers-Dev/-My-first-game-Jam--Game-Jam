using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public bool damaged =false;
    public Rigidbody2D player;
    public float knockUPAmount;
    public bool continuous;
    public float maxTime;
    public float currentTime;
    public bool inside;
    public PlayerStatManager playerStat;
    void Start()
    {
        currentTime = maxTime;
        if(continuous){
            playerStat = FindObjectOfType<PlayerStatManager>();
        }
        playerStat = FindObjectOfType<PlayerStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(continuous&&damaged == true){
            if(currentTime<=0){
                damaged = false;
                currentTime = maxTime;
            }else{
                currentTime -=Time.deltaTime;
            }
        }
        if(inside &&continuous&&damaged ==false){
            playerStat.DamagePlayer(damage);
            damaged = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        inside = false;
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            inside = true;
            PlayerStatManager playStat = col.GetComponent<PlayerStatManager>();
           player = col.GetComponent<Rigidbody2D>();
           GameObject playerObject = col.gameObject;
            if(!damaged){
            playStat.DamagePlayer(damage);
            Debug.Log("damaged once");
            damaged = true;
            player.AddForce(playerObject.transform.up*knockUPAmount,ForceMode2D.Impulse);
            }
        }else{
            Debug.Log("hit but not working");
        }
        
    }
}
