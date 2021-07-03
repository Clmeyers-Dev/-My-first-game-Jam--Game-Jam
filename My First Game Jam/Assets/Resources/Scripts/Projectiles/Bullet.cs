using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 20f;
    public float damage;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right*bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D hit){
       EnemyHealthManager enemy =  hit.GetComponent<EnemyHealthManager>();
       if(enemy !=null){
           enemy.damageEnemy(damage);
       }
        Destroy(gameObject);
    }
}
