using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //Player Variables
    public int level;
    public List<int> levels;
    private Rigidbody2D rb;
    public PlayerMovement playerMovement;
    [SerializeField] public PlayerStatManager playerStatManager;
    public HUDManager hUDManager;
    private PlayerAnimation playerAnimation;
    [Header("MeleeAttackStats")]
    public float swordDamage;
    public Transform attackPoint;
    public float attackRange = .5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public Transform attackUpPoint;
    [Header("Attacking Melee")]
    public bool isAttackPressed;
    public bool isAttacking;
    public float attackDelay = 0.3f;
    [Header("Sound Managment")]
    public bool switchSound = false;
    public SoundManager soundManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerStatManager = GetComponent<PlayerStatManager>();
        hUDManager = GetComponentInChildren<HUDManager>();
    }
    void Start()
    {
        
        int levelCount = SceneManager.sceneCountInBuildSettings ;
        playerStatManager.currentHealth = GlobalControl.instance.hp;

        for (int i = 0; i < levelCount; i++)
        {
            levels.Add(i);
        }

        Scene activeScene = SceneManager.GetActiveScene();
        foreach (int index in levels)
        {
            if (index == activeScene.buildIndex)
            {
                level = index;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            hUDManager.ToggleMenu();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&&!isAttacking)
        {
            isAttackPressed = true;
        }
        if (playerMovement.isWalking)
        {
            playerAnimation.playWalk();
        }
        else
        {
            playerAnimation.playIdle();
        }
    }
    void FixedUpdate()
    {
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {

                isAttacking = true;
                if (playerMovement.isGrounded && !playerMovement.isWalking)
                {
                  // playerAnimation.changeAnimationState("PlayerMeleeAttack");
                    if (!switchSound)
                    {
                        soundManager.playSound("Swing");
                        switchSound = !switchSound;
                    }
                    else
                    {
                        soundManager.playSound("Swing2");
                        switchSound = !switchSound;
                    }
                    Attack();
                }
                if (!playerMovement.isGrounded/*&&!playerMovement.isWalking*/)
                {
                 //  playerAnimation.changeAnimationState("PlayerAirAttack");
                    Attack();
                    if (!switchSound)
                    {
                        soundManager.playSound("Swing");
                        switchSound = !switchSound;
                    }
                    else
                    {
                        soundManager.playSound("Swing2");
                        switchSound = !switchSound;
                    }
                    // playerMovement.r2d.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezePositionX;
                }
                if (playerMovement.isGrounded && playerMovement.isWalking)
                {
                  // playerAnimation.changeAnimationState("PlayerWalkAttack");
                    Attack();
                    if (!switchSound)
                    {
                        soundManager.playSound("Swing");
                        switchSound = !switchSound;
                    }
                    else
                    {
                        soundManager.playSound("Swing2");
                        switchSound = !switchSound;
                    }
                }
                //attackDelay = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("attackComplete", attackDelay);


            }
        }
    }
    void attackComplete(){
        isAttacking = false;
       // fall();
    }
       public void Attack(){
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
           // Debug.Log(enemy.name);
            EnemyHealthManager  eh = enemy.GetComponent<EnemyHealthManager>();
            eh.damageEnemy(swordDamage);
        }
       }
       void OnDrawGizmosSelected(){
    if(attackPoint == null)
    return;
    Gizmos.DrawWireSphere(attackPoint.position,attackRange);
}
    public void savePlayer()
    {
        GlobalControl.instance.hp = playerStatManager.currentHealth;
        GlobalControl.instance.fp = playerMovement.currrentFuleLevel;
    }
}
