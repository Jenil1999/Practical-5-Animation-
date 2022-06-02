using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Exit : MonoBehaviour
{
    [SerializeField] float TimeDelay = 1f;
    [SerializeField] AudioClip ExitSound;
    [SerializeField] Canvas ExitCanvas;
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
        ExitCanvas.gameObject.SetActive(true);
    }
}
