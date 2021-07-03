using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
        // Start is called before the first frame update
        private float Length;
        private float startPos;
        public GameObject cam;
        public float ParallaxEffect;
    void Start()
    {
        cam = Camera.main.gameObject;
        startPos = transform.position.x;
        Length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x*(1-ParallaxEffect));
        float dist = (cam.transform.position.x*ParallaxEffect);
        transform.position = new Vector3(startPos+dist,transform.position.y,transform.position.z);
        if(temp>startPos+Length)startPos+= Length;
        else if(temp<startPos - Length)startPos -=Length;
    }
}
