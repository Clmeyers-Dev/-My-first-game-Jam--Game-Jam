using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveMethods : MonoBehaviour
{
    public static SaveMethods instance;
    public float saveNum;
    public string path;
    public string saveNumString;
    public List<float> saveNums = new List<float>();
    public List<string> filePaths;
    public List<GameObject> buttons;
    public GameObject saveButton;
    public Button saveNameButton;

    // Intializes the saveNums

    void Awake()
    {
        filePaths = Directory.GetFiles(Application.persistentDataPath).ToList();
        buttons = GameObject.FindGameObjectsWithTag("Saves").ToList();
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Intializing savNum variable and converts it to a string for the first time, should only be used when in the main menu.
    public void InitializeSaveNum()
    { 
        saveNum = 0;
        saveNums.Clear();
        filePaths.Clear();
        saveNumString = FloattoString(saveNum);
    }
    
    // Adds to the saveNums list
    public void AddSaveNum(float saveNum)
    {
        saveNums.Add(saveNum);
    }

    // Converts float to string
    public string FloattoString(float num)
    {
        if (num != null)
        {
            string result = num.ToString();

            return result;

        }
        return null;
    }
}
