using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Time Delay Data")]
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] float crashDelay = 2f;

    [Header("SFX Data")]
    [SerializeField] AudioClip victorySFX;
    [SerializeField] [Range(0, 1)] float victorySFXVolume = 0.5f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] [Range(0, 1)] float crashSFXVolume = 0.2f;

    [Header("VFX Data")]
    [SerializeField] ParticleSystem victoryVFX;
    [SerializeField] ParticleSystem crashVFX;

    RocketMovement rocketMovementScript;
    AudioSource myAudioSource;
    BoxCollider myBoxCollider;

    bool isTransitioningStates = false;
    bool isDebugging = false;
    bool collisionsDisabled = false;

    void Start()
    {
        rocketMovementScript = GetComponent<RocketMovement>();
        myAudioSource = GetComponent<AudioSource>();
        myBoxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        ToggleDebugMode();
    }

    void ToggleDebugMode()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (!isDebugging)
            {
                isDebugging = true;
                Debug.Log("Debugging Enabled!");
                return;
            }
            isDebugging = false;
            Debug.Log("Debugging Disabled.");
        }

        if (isDebugging)
        {
            if (Input.GetKeyDown(KeyCode.L))
                LoadNextLevel();

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!collisionsDisabled)
                {
                    collisionsDisabled = true;
                    Debug.Log("Collisions Disabled!");
                    return;
                }
                collisionsDisabled = false;
                Debug.Log("Collisions Enabled.");
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioningStates || collisionsDisabled)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                // TODO: Small Glow VFX?
                break;

            case "Fuel":
                // TODO: Add Fuel
                break;

            case "Finish":
                StartCoroutine(StartVictorySequence(nextLevelDelay));
                break;

            default:
                StartCoroutine(StartCrashSequence(crashDelay));
                break;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        int totalNumberOfScenes = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex == totalNumberOfScenes)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator StartVictorySequence(float waitTime)
    {
        isTransitioningStates = true;

        victoryVFX.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(victorySFX, victorySFXVolume);
        rocketMovementScript.enabled = false;

        yield return new WaitForSeconds(waitTime);

        rocketMovementScript.enabled = true;
        LoadNextLevel();
    }

    IEnumerator StartCrashSequence(float waitTime)
    {
        isTransitioningStates = true;

        crashVFX.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashSFX, crashSFXVolume);
        rocketMovementScript.enabled = false;

        yield return new WaitForSeconds(waitTime);

        rocketMovementScript.enabled = true;
        ReloadLevel();
    }
}
