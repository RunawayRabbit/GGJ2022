using UnityEngine;

public class PlayerSFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _scSfxSource;
    [SerializeField] private AudioClip _exhale;

    public void Exhale()
    {
        _scSfxSource.PlayOneShot(_exhale);
    }
}
