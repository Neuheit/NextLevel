using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic : MonoBehaviour
{
    public Transform previousPlatform;
    //private gameObject []platforms = new gameObject[4];
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("hello");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
