using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

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
	[Space( 10 )]
	[SerializeField] private float fadeDuration = 3.0f;

	private AudioSource _bassSource;
	private AudioSource _loopSource;
	private AudioSource _bassFaderSource;
	private AudioSource _loopFaderSource;

	private musicStates _currentState;
	private void Awake()
	{
		_bassSource      = gameObject.AddComponent<AudioSource>();
		_loopSource      = gameObject.AddComponent<AudioSource>();
		_bassFaderSource = gameObject.AddComponent<AudioSource>();
		_loopFaderSource = gameObject.AddComponent<AudioSource>();

		_bassSource.loop      = true;
		_loopSource.loop      = true;
		_bassFaderSource.loop = true;
		_loopFaderSource.loop = true;

		_bassSource.outputAudioMixerGroup      = musicMixerGroup;
		_loopSource.outputAudioMixerGroup      = musicMixerGroup;
		_bassFaderSource.outputAudioMixerGroup = musicMixerGroup;
		_loopFaderSource.outputAudioMixerGroup = musicMixerGroup;

		_bassSource.clip = _mainBass;
		_loopSource.clip = _mainLoop;
		_currentState    = musicStates.main;
	}

	private void Start()
	{
		PlayStem(musicStates.main);
		_bassSource.Play();
		_loopSource.Play();
	}

	#if UNITY_EDITOR
	private void Update()
	{
		if(Keyboard.current.numpad1Key.wasPressedThisFrame) PlayStem(musicStates.main);
		if(Keyboard.current.numpad2Key.wasPressedThisFrame) PlayStem(musicStates.exploring);
		if(Keyboard.current.numpad3Key.wasPressedThisFrame) PlayStem(musicStates.frightened);
	}
	#endif

	public void PlayStem( musicStates state )
	{
		if( state == _currentState ) return;
		switch( state )
		{
			case musicStates.main:
				PlayMainMusic();
				break;

			case musicStates.exploring:
				PlayExploreMusic();
				break;

			case musicStates.frightened:
				PlayFearMusic();
				break;

			default:
				throw new ArgumentOutOfRangeException( nameof(state), state, null );
		}
	}

	private IEnumerator CrossFadeBass( float duration, AudioClip newClip )
	{
		// Set up the fader
		_bassFaderSource.clip   = newClip;
		_bassFaderSource.time   = _bassSource.time;
		_bassFaderSource.volume = 0.0f;

		_bassFaderSource.Play();

		// Do the crossfade
		StartCoroutine( FadeMusic( _bassSource, duration, 0f ) );
		yield return StartCoroutine( FadeMusic( _bassFaderSource, duration, 1f ) );

		// flip the pointers
		(_bassFaderSource, _bassSource) = (_bassSource, _bassFaderSource);
	}

	private IEnumerator CrossFadeLoop( float duration, AudioClip newClip )
	{
		// Set up the fader
		_loopFaderSource.clip   = newClip;
		_loopFaderSource.time   = _loopSource.time;
		_loopFaderSource.volume = 0.0f;

		_loopFaderSource.Play();

		// Do the crossfade
		StartCoroutine( FadeMusic( _loopSource, duration, 0f ) );
		yield return StartCoroutine( FadeMusic( _loopFaderSource, duration, 1f ) );

		// flip the pointers
		(_loopFaderSource, _loopSource) = (_loopSource, _loopFaderSource);
	}

	private void PlayMainMusic()
	{
		_currentState = musicStates.main;
		this.StopAllCoroutines();
		if( _bassSource.clip != _mainBass )
		{
			StartCoroutine( CrossFadeBass( fadeDuration, _mainBass ) );
		}
		if( _loopSource.clip != _mainLoop )
		{
			StartCoroutine(CrossFadeLoop( fadeDuration, _mainLoop ));
		}
	}

	private void PlayExploreMusic()
	{
		_currentState = musicStates.exploring;
		this.StopAllCoroutines();
		if( _bassSource.clip != _mainBass )
		{
			StartCoroutine(CrossFadeBass( fadeDuration, _mainBass ));
		}
		if( _loopSource.clip != _exploringLoop )
		{
			StartCoroutine(CrossFadeLoop( fadeDuration, _exploringLoop ));
		}
	}

	private void PlayFearMusic()
	{
		_currentState = musicStates.frightened;
		this.StopAllCoroutines();
		if( _bassSource.clip != _fearBass )
		{
			StartCoroutine(CrossFadeBass( fadeDuration, _fearBass ));
		}
		if( _loopSource.clip != _fearLoop )
		{
			StartCoroutine(CrossFadeLoop( fadeDuration, _fearLoop ));
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
	}
}
