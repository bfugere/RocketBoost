using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] float crashDelay = 2f;

    RocketMovement rocketMovementScript;

    void Start()
    {
        rocketMovementScript = GetComponent<RocketMovement>();
    }

    void OnCollisionEnter(Collision other)
    {
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
        // TODO: Add Victory SFX
        // TODO: Add Victory VFX
        rocketMovementScript.GetComponent<AudioSource>().Stop();
        rocketMovementScript.enabled = false;

        yield return new WaitForSeconds(waitTime);

        rocketMovementScript.enabled = true;
        LoadNextLevel();
    }

    IEnumerator StartCrashSequence(float waitTime)
    {
        // TODO: Add Crash SFX
        // TODO: Add Crash VFX
        rocketMovementScript.GetComponent<AudioSource>().Stop();
        rocketMovementScript.enabled = false;

        yield return new WaitForSeconds(waitTime);

        rocketMovementScript.enabled = true;
        ReloadLevel();
    }
}
