using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public int level;
    public float health;
    public float fuel;
    public float[] position;
    public float saveNum;
    public string saveName;

    public PlayerData(PlayerStatManager playerStats, PlayerMovement playerMovement, PlayerManager playerManager)
    {
        health = playerStats.currentHealth;
        fuel = playerMovement.currrentFuleLevel;
        position = new float[3];
        position[0] = playerManager.transform.position.x;
        position[1] = playerManager.transform.position.y;
        position[2] = playerManager.transform.position.z;
        level = playerManager.level;
    }
}

