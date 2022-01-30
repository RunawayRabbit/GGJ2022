using System.Collections;
using UnityEditor.SceneManagement;
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
	[Space(10)]
	[SerializeField] private AudioClip _fearBass;
	[SerializeField] private AudioClip _mainBass;
	[SerializeField] private AudioClip _exploringLoop;
	[Space(10)]
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
		yield return StartCoroutine( FadeMusic( newSource, duration, 1f ) );

		// flip the pointers
		Destroy( _bassSource );
		_bassSource = newSource;
	}

	private IEnumerator CrossFadeLoop( float duration, AudioClip newClip )
	{
		// Create a new AudioSource for the new track
		var newSource = _loopSource = gameObject.AddComponent<AudioSource>();
		newSource.outputAudioMixerGroup = musicMixerGroup;

		newSource.clip   = newClip;
		newSource.time   = _loopSource.time;
		newSource.volume = 0.0f;
		newSource.Play();

		// Do the crossfade
		StartCoroutine( FadeMusic( _loopSource, duration, 0f ) );
		yield return StartCoroutine( FadeMusic( newSource, duration, 1f ) );

		// flip the pointers
		Destroy( _loopSource );
		_loopSource = newSource;
	}

	private void PlayMainMusic()
	{
		if( _bassSource.clip != _mainBass )
		{
			CrossFadeBass( 0.5f, _mainBass );
		}
		if( _loopSource.clip != _mainLoop )
		{
			CrossFadeBass( 0.5f, _mainLoop );
		}
	}

	private void PlayExploreMusic()
	{
		if( _bassSource.clip != _mainBass )
		{
			CrossFadeBass( 0.5f, _mainBass );
		}
		if( _loopSource.clip != _exploringLoop )
		{
			CrossFadeBass( 0.5f, _exploringLoop );
		}
	}

	private void PlayFearMusic()
	{
		if( _bassSource.clip != _mainBass )
		{
			CrossFadeBass( 0.5f, _fearBass );
		}
		if( _loopSource.clip != _mainLoop )
		{
			CrossFadeBass( 0.5f, _fearLoop );
		}
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
