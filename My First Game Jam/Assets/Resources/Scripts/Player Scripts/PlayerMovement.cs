using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    [Header("JetPack")]
    public float MaxFule;
    public float currrentFuleLevel;
    public float jetPackThrust;
    public bool useJetPack;
    public float jetpackCost;
    public float regenAmount;
    public PlayerManager playerManager;
    public PlayerStatManager playerStatManager;
    private Rigidbody2D rigidbody2D;

    [Header("Jumping and Ground detection")]
    public UnityEvent OnLandEvent;
    /*
    *Ceil~Transform located at the top of the player model to check
    * for the ceiling
    *Floor~Transform located at the top of the player model to check
    *for the floor
    */
    [SerializeField] private Transform CeilCheck;
    [SerializeField] private Transform FloorCheck;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    const float CeilRadius = .2f;
    const float GroundRadius = .2f;
    [SerializeField] private LayerMask m_What_Is_Ground;
    public bool isGrounded;
    public bool isWalking;
    public bool isJumpingUp;
    public bool isFalling;
    public bool jump;
    public float jumpHeight;
    public bool jumpend;
    public float ySpeed;
    [SerializeField] public float xDirectionalInput;
    public bool facingRight;
    public float moveDirection = 0;

    [SerializeField] float airMoveSpeed = 10f;
    public float maxSpeed = 3.4f;
    public GameObject playerModel;
    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidbody2D = playerManager.GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
    void Start()
    {
        facingRight = transform.localScale.x > 0;
        currrentFuleLevel = GlobalControl.instance.fp;
    }
    void Update()
    {
        PlayerSpriteCode playerSpriteCode = playerModel.GetComponent<PlayerSpriteCode>();
        if(Input.GetKey(KeyCode.UpArrow)&&currrentFuleLevel>0){
            useJetPack = true;
            currrentFuleLevel -= jetpackCost *Time.deltaTime;
        }else{
            useJetPack = false;
            currrentFuleLevel += regenAmount *Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)&&facingRight){
           playerSpriteCode.flip();
           facingRight = false;
        }
        if(Input.GetKey(KeyCode.D)&&!facingRight){
              playerSpriteCode.flip();
              facingRight = true;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && (isGrounded || Mathf.Abs(rigidbody2D.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            isWalking = true;
        }
        else
        {
            isWalking = false;
            if (isGrounded || rigidbody2D.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }
        //This is the Y velocity of Player rigid body
        ySpeed = rigidbody2D.velocity.y;
        //This is the input from the horizontal value between -1 (left) and 1(right)
        xDirectionalInput = Input.GetAxis("Horizontal");

        //If the player has released the Jump key and their velocity is greater than 0 then their jump can end early
        if (Input.GetKeyUp(KeyCode.Space) && ySpeed > 0)
        {
            jumpend = true;
        }
        else
        {
            jumpend = false;
        }
        //If the Y speed is postive the player is currently moving up and they are not falling
        if (ySpeed > 0)
        {
            isJumpingUp = true;
            isFalling = false;
        }
        else
        {

            isFalling = true;
            isJumpingUp = false;
        }
        //When the player press the jump butten and they are grounded the player jumps else jump is false
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);

        }
        else
        {

            jump = false;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SaveSystem.SavePlayer(playerStatManager, this, playerManager);
        }
    }

   
        void FixedUpdate()
        {
            if(useJetPack){
                
                 rigidbody2D.AddForce(transform.up*jetPackThrust,ForceMode2D.Impulse);
            }
            if (xDirectionalInput != 0)
            {
                isWalking = true;
            }
            #region Checks if Grounded
            bool wasGrounded = isGrounded;
            isGrounded = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(FloorCheck.position, GroundRadius, m_What_Is_Ground);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    isGrounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
            #endregion
            #region  jumping code
            if (isGrounded)
            {
                rigidbody2D.velocity = new Vector2((xDirectionalInput) * maxSpeed, rigidbody2D.velocity.y);
                //var move =new  Vector3 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
                //transform.position += move * maxSpeed * Time.deltaTime;
            }
            else if (!isGrounded)
            {
                rigidbody2D.AddForce(new Vector2(airMoveSpeed * xDirectionalInput, 0));
                //var move =new  Vector3 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
                //transform.position += move * airMoveSpeed * Time.deltaTime;
                if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
                {
                    //var move =new  Vector3 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
                    //transform.position += move * maxSpeed * Time.deltaTime;
                    rigidbody2D.velocity = new Vector2(xDirectionalInput * maxSpeed, rigidbody2D.velocity.y);
                }
            }
            if (jumpend)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * .1f);
            }
            #endregion
        }

    }
