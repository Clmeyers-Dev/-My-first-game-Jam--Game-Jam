using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField]
    private float flashLength = 0f;
    private float flashCounter = 0f;
    public bool flashActive;
    public bool canFlash;
    public SpriteRenderer playerSprite;
    public GameObject floatingPoints;
    public int pointsValue;
    void Start(){
        playerSprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (canFlash)
        {
            if (flashActive)
            {
                if (flashCounter > flashLength * .99f)
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                }
                else if (flashCounter > flashLength * .82f)
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                }
                else if (flashCounter > flashLength * .66f)
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                }
                else if (flashCounter > flashLength * .49f)
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                }
                else if (flashCounter > flashLength * .33f)
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                }
                else if (flashCounter > flashLength * .16f)
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                }
                else if (flashCounter > 0f)
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
                }
                else
                {
                    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                    flashActive = false;
                }
                flashCounter -= Time.deltaTime;
            }
        }
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void damageEnemy(float damage)
    {
//        Debug.Log("hurtSomthing");
        currentHealth -= damage;
        if (canFlash)
        {
            flashActive = true;
            flashCounter = flashLength;
        }

    }
    public void die()
    {
      GameObject points =   Instantiate(floatingPoints,transform.position,Quaternion.identity);
      points.transform.GetChild(0).GetComponent<TextMesh>().text = "+"+pointsValue;
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        Destroy(this.gameObject);
    }
}
