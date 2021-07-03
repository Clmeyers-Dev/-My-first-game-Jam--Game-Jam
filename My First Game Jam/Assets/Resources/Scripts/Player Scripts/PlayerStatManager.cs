using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth;
    public float currentHealth;
    public float flashLength = 0f;
    private float flashCounter = 0f;
    public SpriteRenderer playerSprite;
	public bool flashActive;
    void Start()
    {

      playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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

    // Damages the player with a set amount of damage.
    public void DamagePlayer(float amount)
    {
            flashActive = true;
            flashCounter = flashLength;
        if(currentHealth > 0)
        {
            currentHealth-= amount;
        }
        if(currentHealth<=0){
            KillPlayer();
        }
      
    }
   
void KillPlayer(){
    Debug.Log("You have died");
}
    // Heals the player for a certain amount of health
  public void HealPlayer(float amount)
    {
        if(currentHealth < maxHealth && (currentHealth + amount) <= maxHealth)
        {
            currentHealth +=amount*Time.deltaTime;
        }
        else if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
