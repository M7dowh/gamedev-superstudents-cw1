using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRB;
    AudioSource rocketAudioSource;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] GameObject winningFX;
    [SerializeField] GameObject lossingFX;
    bool isControlEnabled = true;
    int loadingTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRB.AddRelativeForce(Vector3.up * 10);
            rocketAudioSource.PlayOneShot(mainEngine);
           // if (!rocketAudioSource.isPlaying)
          //  {
              //  rocketAudioSource.PlayOneShot(mainEngine);
            //}
        //}
        //else
        //{
           // rocketAudioSource.Stop();
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }



    void Rotate()
    {
        rocketRB.freezeRotation = true; //this says that we will take manual control of the rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * 0.25f);
        }
        else
            if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * 0.25f);
        }
        rocketRB.freezeRotation = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                print("You win!");
                winningFX.SetActive(true);
                isControlEnabled = false;
                Invoke("LoadNextScene", loadingTime);
                break;



            default:
                lossingFX.SetActive(true);
                Invoke("LoadFirstLevel", loadingTime);
                isControlEnabled = false;
                break;
        }
    }

}

