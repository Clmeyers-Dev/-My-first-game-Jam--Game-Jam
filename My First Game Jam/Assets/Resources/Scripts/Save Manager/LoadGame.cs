using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    private PlayerStatManager playerStatManager;
    private PlayerMovement playerMovement;
    private HUDManager hudManager;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.gameObject.GetComponent<PlayerManager>();
        playerStatManager = player.gameObject.GetComponent<PlayerStatManager>();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        hudManager = GameObject.Find("PlayerHUD").GetComponent<HUDManager>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerStatManager, playerMovement, playerManager, SaveMethods.instance);
    }

    public void LoadPlayer(Button button)
    {
        //playerManager.savePlayer();

        var saveMethods = SaveMethods.instance;
        var buttonName = button.name;
        var saveNumChars = buttonName.ToCharArray(buttonName.Length - 1, 1);
        var saveNum = float.Parse(saveNumChars[0].ToString());
        var data = SaveSystem.LoadPlayer(SaveSystem.FindPath(saveNum));

        // playerStatManager.currentHealth = data.health;
        //playerMovement.currrentFuleLevel = data.fuel;
        
        // Loads the previous scene where the player was before they died and had to reload.
        SceneManager.LoadScene(data.level);
        
        // Gets the old position from the position float array. then sets it to the player's position.
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        
        
    }

    // Deletes the player's save when the delete button is pushed.
    public void DeletePlayerSave(Button buttonb)
    {
        if (buttonb == null) return;

        var saveNumChars = buttonb.name.ToCharArray(buttonb.name.Length - 1, 1);
        var saveNum = float.Parse(saveNumChars[0].ToString());
        SaveSystem.DeleteSave(SaveSystem.FindPath(saveNum));
    }
}
