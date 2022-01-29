using System.Collections;
using UnityEngine;

public enum musicStates
{
    main,
    exploring,
    frightened
}

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip _fearBass;
    [SerializeField] private AudioClip _mainBass;
    [SerializeField] private AudioClip _exploringLoop;
    [SerializeField] private AudioClip _fearLoop;
    
    [SerializeField] private AudioSource _mainSource;
    [SerializeField] private AudioSource _bassSource;
    [SerializeField] private AudioSource _loopSource;

    private void Start()
    {
        _bassSource.Play();
        _mainSource.Play();
    }

    public void PlayStem(musicStates state)
    {
        if (state == musicStates.exploring)
        {
            PlayExploreMusic();
        }
        
        else if (state == musicStates.frightened)
        {
            PlayFearMusic();
        }
        
        else if (state == musicStates.main)
        {
            PlayMainMusic();
        }
    }

    private void PlayMainMusic()
    {
        StartCoroutine(FadeMusic(_loopSource, 0.5f, 0f));
        StartCoroutine(FadeMusic(_bassSource, 0.5f, 0f));
        _loopSource.Stop();
        _bassSource.time = _mainSource.time;
        _bassSource.clip = _mainBass;
        StartCoroutine(FadeMusic(_bassSource, 0.5f, 1f));
    }

    private void PlayExploreMusic()
    {
        StartCoroutine(FadeMusic(_loopSource, 0.5f, 0f));
        _loopSource.time = _mainSource.time;
        _loopSource.clip = _exploringLoop;
        _loopSource.Play();
        StartCoroutine(FadeMusic(_loopSource, 0.5f, 1f));
    }

    private void PlayFearMusic()
    {
        StartCoroutine(FadeMusic(_loopSource, 0.5f, 0f));
        StartCoroutine(FadeMusic(_bassSource, 0.5f, 0f));
        _bassSource.clip = _fearBass;
        _bassSource.time = _mainSource.time;
        _loopSource.clip = _fearLoop;
        _loopSource.Play();
        StartCoroutine(FadeMusic(_loopSource, 0.5f, 1f));
        StartCoroutine(FadeMusic(_bassSource, 0.5f, 1f));
    }

    private IEnumerator FadeMusic(AudioSource source, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = source.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float percentageComplete = currentTime / duration;

            source.volume = Mathf.Lerp(start, targetVolume, percentageComplete);

            yield return null;
        }
        yield break;
    }
}
