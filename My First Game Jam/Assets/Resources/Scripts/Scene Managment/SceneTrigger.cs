using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneEnum sceneToLoad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadWithbutton(){
        PlayerManager playman = FindObjectOfType<PlayerManager>();
       if(playman!=null){
        playman.savePlayer();
       }
        Load.LoadThis(sceneToLoad);
    }
    void OnCollisionEnter2D(Collision2D col){
        PlayerManager playman = FindObjectOfType<PlayerManager>();
        playman.savePlayer();
        Debug.Log("hit");
        if(col.transform.tag == "Player"){
        Load.LoadThis(sceneToLoad);
        }
    }
    void OnTriggerEnter2D(Collider2D col){
       
        PlayerManager playman = FindObjectOfType<PlayerManager>();
        playman.savePlayer();
        
        if(col.transform.tag == "Player"){
            Debug.Log("hit");
        Load.LoadThis(sceneToLoad);
        }
    }

    public void loadWithCode(){
        PlayerManager playman = FindObjectOfType<PlayerManager>();
        if (playman != null)
        {
            playman.savePlayer();
        }

        Load.LoadThis(sceneToLoad);
//        SaveMethods.instance.InitializeSaveNum();
    }

}
