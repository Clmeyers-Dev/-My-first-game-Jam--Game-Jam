using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteCode : MonoBehaviour
{
    // Start is called before the first frame update
    public bool facingRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void flip(){
         facingRight = !facingRight;
       transform.Rotate(0f,180f,0f);
    }
}
