using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody _rigidBody;
    AudioSource _audioSource;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        Rotate();
        Thrust();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                print("Complete");
                SceneManager.LoadScene(1);
                break;
            default:
                print("DEAD");
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void Rotate()
    {
        _rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        _rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

        }
        else
        {
            _audioSource.Stop();
        }
    }
}
