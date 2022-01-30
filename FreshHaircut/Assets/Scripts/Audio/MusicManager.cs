using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum musicStates
{
	main,
	exploring,
	frightened
}

public class MusicManager : MonoBehaviour
{
	[SerializeField] private AudioMixerGroup musicMixerGroup;

	[SerializeField] private AudioClip _fearBass;
	[SerializeField] private AudioClip _mainBass;
	[SerializeField] private AudioClip _exploringLoop;
	[SerializeField] private AudioClip _fearLoop;
	[SerializeField] private AudioClip _mainLoop;

	private AudioSource _mainSource;
	private AudioSource _bassSource;
	private AudioSource _loopSource;

	private void Awake()
	{
		_mainSource = gameObject.AddComponent<AudioSource>();
		_bassSource = gameObject.AddComponent<AudioSource>();
		_loopSource = gameObject.AddComponent<AudioSource>();
	}

	private void Start()
	{
		_bassSource.clip = _mainBass;
		_loopSource.clip = _mainLoop;

		_mainSource.outputAudioMixerGroup = musicMixerGroup;
		_bassSource.outputAudioMixerGroup = musicMixerGroup;
		_loopSource.outputAudioMixerGroup = musicMixerGroup;
	}

	public void PlayStem( musicStates state )
	{
		if( state == musicStates.exploring ) { PlayExploreMusic(); }

		else if( state == musicStates.frightened ) { PlayFearMusic(); }

		else if( state == musicStates.main ) { PlayMainMusic(); }
	}

	private IEnumerator CrossFadeBass( float duration, AudioClip newClip )
	{
		// Create a new AudioSource for the new track
		var newSource = _bassSource = gameObject.AddComponent<AudioSource>();
		newSource.outputAudioMixerGroup = musicMixerGroup;

		newSource.clip   = newClip;
		newSource.time   = _bassSource.time;
		newSource.volume = 0.0f;
		newSource.Play();

		// Do the crossfade
		StartCoroutine( FadeMusic( _bassSource, duration, 0f ) );
		StartCoroutine( FadeMusic( _loopSource, duration, 0f ) );
	}

	private void PlayMainMusic()
	{
		StartCoroutine( FadeMusic( _loopSource, 0.5f, 0f ) );
		StartCoroutine( FadeMusic( _bassSource, 0.5f, 0f ) );
		_loopSource.time = _mainSource.time;
		_loopSource.clip = _mainLoop;
		_bassSource.time = _mainSource.time;
		_bassSource.clip = _mainBass;
		StartCoroutine( FadeMusic( _bassSource, 0.5f, 1f ) );
		StartCoroutine( FadeMusic( _loopSource, 0.5f, 1f ) );
	}

	private void PlayExploreMusic()
	{
		StartCoroutine( FadeMusic( _loopSource, 0.5f, 0f ) );
		_loopSource.time = _mainSource.time;
		_loopSource.clip = _exploringLoop;
		_loopSource.Play();
		StartCoroutine( FadeMusic( _loopSource, 0.5f, 1f ) );
	}

	private void PlayFearMusic()
	{
		StartCoroutine( FadeMusic( _loopSource, 0.5f, 0f ) );
		StartCoroutine( FadeMusic( _bassSource, 0.5f, 0f ) );
		_bassSource.clip = _fearBass;
		_bassSource.time = _mainSource.time;
		_loopSource.clip = _fearLoop;
		_loopSource.Play();
		StartCoroutine( FadeMusic( _loopSource, 0.5f, 1f ) );
		StartCoroutine( FadeMusic( _bassSource, 0.5f, 1f ) );
	}

	private IEnumerator FadeMusic( AudioSource source, float duration, float targetVolume )
	{
		float currentTime = 0;
		float start       = source.volume;

		while( currentTime < duration )
		{
			currentTime += Time.deltaTime;
			float percentageComplete = currentTime / duration;

			source.volume = Mathf.Lerp( start, targetVolume, percentageComplete );

			yield return null;
		}

		yield break;
	}
}
