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
    public string[] filePaths;
    public List<GameObject> buttons;
    public GameObject saveButton;
    public GameObject saveInput;
    public InputField saveInputField;
    
    // Intializes the saveNums

    void Awake()
    {
        saveInput = GameObject.Find("Save Name");
        saveInputField = saveInput.GetComponent<InputField>();
        filePaths = Directory.GetFiles(Application.persistentDataPath);
        buttons = GameObject.FindGameObjectsWithTag("Saves").ToList();
    }
    
    void InitializeSaveNum()
    { 
        saveNum = 0;
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
