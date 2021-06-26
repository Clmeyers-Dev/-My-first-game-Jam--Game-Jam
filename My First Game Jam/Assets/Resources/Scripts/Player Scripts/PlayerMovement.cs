using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    
    private PlayerManager playerManager;
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


    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
         if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
    void FixedUpdate()
    {
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
    }
    void Update()
    {

    }
}
