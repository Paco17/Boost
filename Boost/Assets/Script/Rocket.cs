using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 80f;
    [SerializeField] float mainThrust = 80f;
    Rigidbody rigidbody;
    AudioSource audio;

    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Dead":
                print("Dead");
                break;
            default:
                print("Unknown");
                break;
        }
    }

    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space)) //Can thrust while rotating
        {
            rigidbody.AddRelativeForce(Vector3.up*mainThrust);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;//Take manual control of rotation

      
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
           
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(Vector3.back*rotationThisFrame);
        }

        rigidbody.freezeRotation = false; //Resume physics control of rotation
    }

   
}
