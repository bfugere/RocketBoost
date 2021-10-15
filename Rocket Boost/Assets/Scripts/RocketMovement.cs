using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [Header("Movement Data")]
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;

    [Header("SFX Data")]
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] [Range(0, 1)] float thrustSFXVolume = 1f;

    [Header("VFX Data")]
    [SerializeField] ParticleSystem baseThrusterVFX;
    [SerializeField] ParticleSystem leftThrusterVFX;
    [SerializeField] ParticleSystem rightThrusterVFX;

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
        {
            ApplyRotation(Vector3.forward);
            if (!rightThrusterVFX.isPlaying)
                rightThrusterVFX.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
            if (!leftThrusterVFX.isPlaying)
                leftThrusterVFX.Play();
        }
        else
        {
            rightThrusterVFX.Stop();
            leftThrusterVFX.Stop();
        }
    }

    void ThrustRocket()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!myAudioSource.isPlaying)
                myAudioSource.PlayOneShot(thrustSFX, thrustSFXVolume);

            if (!baseThrusterVFX.isPlaying)
                baseThrusterVFX.Play();
        }
        else
        {
            myAudioSource.Stop();
            baseThrusterVFX.Stop();
        }
    }

    void ApplyRotation(Vector3 vector)
    {
        myRigidbody.freezeRotation = true; // Manually overrides physics conflict.
        transform.Rotate(vector * rotationThrust * Time.deltaTime);
        myRigidbody.freezeRotation = false; // Enable physics system again.
    }
}
