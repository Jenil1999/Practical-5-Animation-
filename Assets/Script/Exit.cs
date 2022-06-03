using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float TimeDelay = 1f;
    [SerializeField] AudioClip ExitSound;
    
    public AudioSource Audio;

    public static Exit Instant;
    private void Awake()
    {
        Instant = this;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        Audio.mute = true;
        AudioSource.PlayClipAtPoint(ExitSound, Camera.main.transform.position);
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(TimeDelay);
        SceneManager.LoadScene(1);
    }
   public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
