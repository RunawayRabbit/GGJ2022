using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class PlayerSFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _scSfxSource;
    [SerializeField] private AudioClip _exhale;

    public void Exhale()
    {
        _scSfxSource.clip = _exhale;
        _scSfxSource.Play();
    }
}
