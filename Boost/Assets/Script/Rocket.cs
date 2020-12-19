using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 80f;
    [SerializeField] float mainThrust = 80f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip dead;
    [SerializeField] AudioClip win;

    Rigidbody rigidbody;
    AudioSource audio;
    static int indexScene = 0;

    enum State {Alive , Dying, Trascending};
    State state = State.Alive;

    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        //todo somewhere stop the sound on death
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }
        audio.Stop();
        switch (collision.gameObject.tag)
        {
            
            case "Finish":
                LevelComplete();
                break;
            case "Dead":
                Dying();
                break;
            default:
                print("Unknown");
                break;
        }
    }

    private void LevelComplete()
    {
        state = State.Trascending;
        playAudio(win);
        Invoke("NextLevel", 3f);
    }

    private void Dying()
    {
        state = State.Dying;
        playAudio(dead);
        Invoke("RepeatLevel", 3f);
    }

    private void playAudio(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    private void RepeatLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void NextLevel()
    {
        // todo allow for more than 2 levels 
        indexScene++;
        SceneManager.LoadScene(indexScene); 
    }

    private void RespondToThrustInput()
    {

        if (Input.GetKey(KeyCode.Space)) //Can thrust while rotating
        {
            ApplyThrust();
        }
        else
        {
            audio.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audio.isPlaying)
        {
            playAudio(mainEngine);
        }
    }

    private void RespondToRotateInput()
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
