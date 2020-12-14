using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour{

    Rigidbody rigidbody;
    AudioSource audio;
    
    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) //Can thrust while rotating
        {
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audio.isPlaying){
                audio.Play();
            }
        }
        else{
            audio.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back);
        }
    }
}
