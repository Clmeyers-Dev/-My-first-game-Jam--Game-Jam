using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public FloatTowards floatTowards;
    public float meleeRange;
    public Animator GhostAnimator;
    [Header("MeleeAttackStats")]
    public float meleeDamage;
    public Transform attackPoint;
    public float attackRange = .5f;
    public LayerMask enemyLayers;
    public bool cantAttack;
    public string currentState;
    public float attackDelay;

    void Awake()
    {
        floatTowards = GetComponent<FloatTowards>();
        GhostAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (floatTowards.distance < meleeRange)
        {
            if (!cantAttack)
            {
                changeAnimationState("GhostMeleeAttack");
                cantAttack = true;
               // Attack();
                Invoke("attackComplete", attackDelay);
            }
        }
        else
        {
            changeAnimationState("Idle");
        }
    }
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug.Log(enemy.name);
            PlayerStatManager ps = enemy.GetComponentInParent<PlayerStatManager>();
            ps.DamagePlayer(meleeDamage);
        }
    }
    void attackComplete()
    {
        cantAttack = false;
        changeAnimationState("Idle");
       
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
