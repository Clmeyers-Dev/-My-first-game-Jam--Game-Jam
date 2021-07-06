using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingGhostBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isJumpingUp;
    public float YVelocity;
    public bool isGrounded;
    public bool onThePlayer;
    public Transform groundPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public Rigidbody2D rb;
    public float jumpForce;
    public bool isJumping;
    public Animator animator;
    public float maxTime;
    public float currentTime;
     [Header("MeleeAttackStats")]
    public float meleeDamage;
    public Transform attackPoint;
    public float attackRange = .5f;
    public LayerMask enemyLayers;
    public bool hasAttacked;
    public GameObject Player;
    public Vector3 target;
    public float speed;
    public float range;
    public float distance;
   
    void Start()
    {
        Player = FindObjectOfType<PlayerManager>().gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
         distance = Vector3.Distance(transform.position,Player.transform.position);
        if(distance<range){
      if(Player.transform.position.x>transform.position.x){
          //move right
          rb.velocity = new Vector2(1*speed,rb.velocity.y);
      }else{
          //move left
          rb.velocity = new Vector2(-1*speed,rb.velocity.y);
      }
      
        isGrounded = Physics2D.OverlapCircle(groundPos.position,checkRadius,whatIsGround);
        onThePlayer = Physics2D.OverlapCircle(groundPos.position,checkRadius,whatIsPlayer);
        if(isGrounded){
           if(currentTime <=0){
                jump();
                currentTime = maxTime;
           }else{
               currentTime-=Time.deltaTime*10;
           }
            
        }else{
            isJumping = false;
        }
        }
        animator.SetBool("isJumping",isJumping);
        YVelocity = rb.velocity.y;
        animator.SetFloat("YVelocity",YVelocity);
        animator.SetBool("isGrounded",isGrounded);
         
       
    }
    public void jump(){
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
           
           
    }
     public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug.Log(enemy.name);
            PlayerStatManager ps = enemy.GetComponentInParent<PlayerStatManager>();
            ps.DamagePlayer(meleeDamage);
            hasAttacked = false;
        }
    }
   
     void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
