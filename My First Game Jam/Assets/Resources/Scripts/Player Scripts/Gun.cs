using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform SpawnPoint;
    public GameObject bullet;
    public bool isAttacking;
    public bool facingRight = true;
    [SerializeField] private float attackDelay;
    public SpriteRenderer gunSprite;
    private void Awake(){
    gunSprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    // gunSprite.flipX = facingRight;
        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x -gunPos.x;
        mousePos.y = mousePos.y - gunPos.y;
        float angle = Mathf.Atan2(mousePos.y,mousePos.x)*Mathf.Rad2Deg;
        if(!facingRight){
            transform.rotation = Quaternion.Euler(new Vector3(180f,0f,-angle));
        }else{
            transform.rotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        }
       
          if(Input.GetKey(KeyCode.A)&&facingRight){
           flip();
           facingRight = false;
        }
        if(Input.GetKey(KeyCode.D)&&!facingRight){
            flip();
            facingRight = true;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0)&&!isAttacking){
            shoot();
        }
      
    }
    void shoot(){
        isAttacking = true;
        Instantiate(bullet,SpawnPoint.position,SpawnPoint.rotation);
        Invoke("attackComplete", attackDelay);
    }
    public void flip(){
         facingRight = !facingRight;
       transform.Rotate(0f,180f,0f);
    }
    void attackComplete(){
        isAttacking = false;
       // fall();
    }
}
