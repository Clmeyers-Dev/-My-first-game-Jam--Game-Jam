using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public  AudioSource audioSource;
    public AudioClip shoot;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
         
    }
    public void playShot(){
audioSource.Play();
    }
    public void playSound(string soundName){
        AudioClip soundToPlay = Resources.Load<AudioClip>("SoundEffects/Player/"+soundName);
        audioSource.PlayOneShot(soundToPlay);
        Debug.Log("worked");
    }
}
