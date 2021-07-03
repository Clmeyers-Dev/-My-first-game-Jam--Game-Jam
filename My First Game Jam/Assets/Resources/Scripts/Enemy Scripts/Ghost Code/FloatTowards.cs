using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTowards : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float distance;
    public GameObject Player;
    public bool facingRight;
    void Start()
    {
        Player = FindObjectOfType<PlayerManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPostion = Player.transform.position;
        Vector3 ThisPostion = transform.position;
        distance = Vector3.Distance(playerPostion, transform.position);
        if (distance > 0)
        {
            transform.position = Vector3.MoveTowards(ThisPostion, playerPostion, speed);
        }


        if (transform.position.x > Player.transform.position.x && !facingRight)
        {
            facingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        if (transform.position.x < Player.transform.position.x && facingRight)
        {
            facingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }
}
