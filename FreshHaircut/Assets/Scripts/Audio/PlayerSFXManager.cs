using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _scSfxSource;
    [SerializeField] private AudioClip _exhale;
    [SerializeField] private AudioClip _footstep;

    public void Exhale()
    {
        _scSfxSource.clip = _exhale;
        _scSfxSource.Play();
    }

    public void Walk(bool isWalking)
    {
        if (isWalking == true)
        {
            _sfxSource.clip = _footstep;
            StartCoroutine(Walking());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Walking()
    {
        while (true)
        {
            _sfxSource.pitch = Random.Range(0.97f, 1.02f);
            _sfxSource.volume = Random.Range(0.2f, 0.4f);
            _sfxSource.Play();
            yield return new WaitForSeconds(0.4f);
        }
    }
}
