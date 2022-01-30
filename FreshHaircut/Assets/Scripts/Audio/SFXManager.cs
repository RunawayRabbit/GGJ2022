using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXManager : MonoBehaviour
{
	private static SFXManager _instance = null;

	private readonly List<AudioSource> _sourcePool = new List<AudioSource>();

	private void Awake()
	{
		if( _instance == null ) _instance = this;

	}

	public static AudioSource PlaySoundAt( AudioData audioData,
										   Vector3   position,
										   float     volume = 1.0f,
										   float     delay  = 0.0f )
	{
		AudioSource audioSource;

		lock(_instance)
		{
			if( _instance._sourcePool.Count > 0 )
			{
				int lastElement = _instance._sourcePool.Count - 1;
				audioSource = _instance._sourcePool[lastElement];
				_instance._sourcePool.Remove( audioSource );

			}
			else
				audioSource = GenerateNewAudioSource();
		}

		var trans = audioSource.transform;
		trans.position = position;

		if( delay == 0.0f )
		{
			_instance.StartCoroutine( PlaySoundAndReturnToPool( audioSource, audioData, volume ) );
		}
		else
		{
			_instance.StartCoroutine( PlaySoundDelayed( audioSource, audioData, volume, delay ) );
		}

		return audioSource;
	}

	private static IEnumerator PlaySoundAndReturnToPool( AudioSource audioSource, AudioData audioData, float volume )
	{
		float duration = audioData.PlayOn( audioSource, volume );

		yield return new WaitForSeconds( duration );

		_instance._sourcePool.Add( audioSource );
	}

	private static IEnumerator PlaySoundDelayed( AudioSource audioSource,
												 AudioData   AudioData,
												 float       volume,
												 float       delay )
	{
		yield return new WaitForSeconds( delay );
		yield return PlaySoundAndReturnToPool( audioSource, AudioData, volume );
	}

	private static AudioSource GenerateNewAudioSource()
	{
		var GO = new GameObject( $"SFX {Random.Range( 1, 99 )}" );
		//var GO = new GameObject( "SFX" );
		GO.transform.parent = _instance.transform;
		var source = GO.AddComponent<AudioSource>();

		return source;
	}
}
