using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator playerAnimator;
    
    public string currentState;
    void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }
    public void playWalk(){
        changeAnimationState("Walk");
    }
    public void playIdle(){
        changeAnimationState("Idle");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
     public void changeAnimationState(string newState){
        
        //Stop an animation from interrupting itself
        if(currentState == newState)return;
    
       //play the target animation
        playerAnimator.Play(newState);

        //Ressaign the Current State
        currentState = newState;
    }
}
