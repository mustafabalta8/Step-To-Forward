using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //paramethers
    [SerializeField] float LevelLoadDelay=1f;
    [SerializeField] AudioClip DeathSFX;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem SuccessParticle;

    [SerializeField] AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Update()
    {
        RespondToDebugKeys();

    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
            Debug.Log("collisionDisabled:" + collisionDisabled);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (collisionDisabled || isTransitioning) { return; }

        //if(isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("friend");
                break;
            case "Finish":
                StartSuccesSequence();
                break;
            case "Fuel":
                Debug.Log("fuel");
                break;
            default:

                StartCrashSequence();
                break;


        }
    }
    
    void StartSuccesSequence()
    {
        SuccessParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        //if (!audioSource.isPlaying)
            audioSource.PlayOneShot(SuccessSFX);
        Invoke("LoadNextLevel", 1f);
    }
    void StartCrashSequence()
    {
        DeathParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        //todo add sfx ,vfx
        //if (!audioSource.isPlaying)
            audioSource.PlayOneShot(DeathSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", 1f);
    }
    void ReloadScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentIndex + 1);
        }

    }
}
