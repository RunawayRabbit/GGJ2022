using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	private static GameObject _player;
	private Vector3 _startDir;

	private void Awake()
	{
		if( !_player )
		{
			_player   = GameObject.FindWithTag( "Player" );
		}
		_startDir = transform.forward;

	}

	private void Update()
	{
		var targetLocation = _player.transform.position;
		if(Vector3.Dot( _startDir, (targetLocation - transform.position ).normalized ) > 0.65f)
			transform.LookAt(targetLocation + Vector3.down, Vector3.up);
	}
}
