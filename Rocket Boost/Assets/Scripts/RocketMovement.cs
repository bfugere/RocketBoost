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
            RotateRocketLeft();
        else if (Input.GetKey(KeyCode.D))
            RotateRocketRight();
        else
            StopRocketRotation();
    }

    void ThrustRocket()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            StartThrusting();
        else
            StopThrusting();
    }

    void RotateRocketLeft()
    {
        ApplyRotation(Vector3.forward);
        if (!rightThrusterVFX.isPlaying)
            rightThrusterVFX.Play();
    }

    void RotateRocketRight()
    {
        ApplyRotation(Vector3.back);
        if (!leftThrusterVFX.isPlaying)
            leftThrusterVFX.Play();
    }

    void ApplyRotation(Vector3 vector)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(vector * rotationThrust * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }

    void StopRocketRotation()
    {
        rightThrusterVFX.Stop();
        leftThrusterVFX.Stop();
    }

    void StartThrusting()
    {
        myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!myAudioSource.isPlaying)
            myAudioSource.PlayOneShot(thrustSFX, thrustSFXVolume);

        if (!baseThrusterVFX.isPlaying)
            baseThrusterVFX.Play();
    }

    void StopThrusting()
    {
        myAudioSource.Stop();
        baseThrusterVFX.Stop();
    }
}
