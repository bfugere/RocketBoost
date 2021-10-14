using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RotateRocket();
        ThrustRocket();
    }

    void RotateRocket()
    {
        if (Input.GetKey(KeyCode.A))
            ApplyRotation(Vector3.forward);

        else if (Input.GetKey(KeyCode.D))
            ApplyRotation(Vector3.back);
    }

    void ThrustRocket()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            // Thrust SFX
            if (!myAudioSource.isPlaying)
                myAudioSource.Play();
        }
        else
            myAudioSource.Stop();
    }

    void ApplyRotation(Vector3 vector)
    {
        myRigidbody.freezeRotation = true; // Manually overrides physics conflict.
        transform.Rotate(vector * rotationThrust * Time.deltaTime);
        myRigidbody.freezeRotation = false; // Enable physics system again.
    }
}
