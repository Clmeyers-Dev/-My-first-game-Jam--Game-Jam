using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthBar;
    public PlayerManager playerManager;
    private PlayerStatManager playerStatManager;
    public Slider fuleBar;
    public bool menuUp;
    public bool saveUp;
    public GameObject Menu;
    public GameObject savesmenu;
    private GlobalControl globalControl = GlobalControl.instance;
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerStatManager = playerManager.playerStatManager;
        healthBar.maxValue = playerStatManager.maxHealth;
        fuleBar.maxValue = playerManager.playerMovement.MaxFule;
        menuUp = false;
        Menu.SetActive(false);
      //  savesmenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerStatManager.currentHealth;
        fuleBar.value = playerManager.playerMovement.currrentFuleLevel;
    }
    public void ToggleMenu(){
        menuUp=!menuUp;
        Menu.SetActive(menuUp);
    }

    public void OpenSaveMenu()
    {
            saveUp = true;
            menuUp = !menuUp;
            Menu.SetActive(menuUp);
            savesmenu.SetActive(saveUp);
    }

    public void CloseSaveMenu()
    {
        saveUp = false;
        menuUp = !menuUp;
        Menu.SetActive(menuUp);
        savesmenu.SetActive(saveUp);
    }
}
