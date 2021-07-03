using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningGhostBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator GhostAnimator;
    public string currentState;
    public GameObject Player;
    public float distance;
    public float speed;
    public float range;
    public LayerMask groundLayer;
    public GameObject shockwave;
   public float rayDistance;
   public Transform spawnpoint;
   public Vector3 left = new Vector3(0,0,0);
   public Vector3 right = new Vector3(0,180,0);
   public bool flipped;
 
    void Start()
    {
        Player = FindObjectOfType<PlayerManager>().gameObject;
        GhostAnimator = GetComponent<Animator>();
      
    }
    bool IsGrounded() {
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    RaycastHit2D hit = Physics2D.Raycast(position, direction, rayDistance, groundLayer);
    if (hit.collider != null) {
        return true;
    }
    
    return false;
}
    // Update is called once per frame
    public float maxTime;
    public float currentTime;
    void Update()
    {
        if(Player.transform.position.x<transform.position.x){
            transform.eulerAngles = left;
        }else{
            transform.eulerAngles = right;
        }
          if(transform.eulerAngles.y >=180){
            flipped = true;
        }
        Vector3 playerPostion = Player.transform.position;
        Vector3 ThisPostion = transform.position;
        distance = Vector3.Distance(playerPostion, transform.position);
        if (distance < range)
        {
                Invoke("dive", .5f);
            
        }
        else
        {
            spin();
        }
    }
    public void spin()
    {
        changeAnimationState("Spin");


    }
    public void dive()
    {
        changeAnimationState("dive");
       
        if(IsGrounded()){
           
           spawnShockWave();
           
            Destroy(gameObject,0);
        }else{
           transform.position += -transform.up *speed*10*Time.deltaTime;
         
        }
    }
    void spawnShockWave(){
GameObject shockwaveObject =  Instantiate(shockwave,spawnpoint.position,Quaternion.identity);
if(flipped){
    Vector3 currentEulerAngles = new Vector3(0,0,0);
    shockwaveObject.transform.eulerAngles = currentEulerAngles;
}else{
    Vector3 currentEulerAngles2 = new Vector3(0,180,0);
    shockwaveObject.transform.eulerAngles = currentEulerAngles2;
}
    }
    public void changeAnimationState(string newState)
    {

        //Stop an animation from interrupting itself
        if (currentState == newState) return;

        //play the target animation
        GhostAnimator.Play(newState);

        //Ressaign the Current State
        currentState = newState;
    }
}
