using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 80f;
    [SerializeField] float mainThrust = 80f;
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
            Thrust();
            Rotate();
        }
        else
        {
            audio.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }

        switch (collision.gameObject.tag)
        {
            case "Finish":
                state = State.Trascending;
                print("Friendly");
                Invoke("NextLevel", 3f);
                break;
            case "Dead":
                state = State.Dying;
                print("Dead");
                Invoke("RepeatLevel", 2f);
                break;
            default:
                print("Unknown");
                break;
        }
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
