using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    private PlayerStatManager playerStatManager;
    private PlayerMovement playerMovement;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.gameObject.GetComponent<PlayerManager>();
        playerStatManager = player.gameObject.GetComponent<PlayerStatManager>();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerStatManager, playerMovement, playerManager);
    }
    
    public void LoadPlayer()
    {
        playerManager.savePlayer();
        
        PlayerData data = SaveSystem.LoadPlayer();

        playerStatManager.currentHealth = data.health;
        playerMovement.currrentFuleLevel = data.fuel;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        
        SceneManager.LoadScene(data.level);
    }
}
