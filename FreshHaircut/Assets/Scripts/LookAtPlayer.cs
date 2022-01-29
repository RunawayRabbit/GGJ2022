using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	private GameObject _player;
	private Vector3 _startDir;
	private void Awake()
	{
		_player  = GameObject.FindWithTag( "Player" );
		_startDir = transform.forward;
	}

	void Update()
	{
		var targetLocation = _player.transform.position + Vector3.up;
		if(Vector3.Dot( _startDir,
						(_startDir - targetLocation).normalized) > 0.65f)

		{
			transform.LookAt(targetLocation, Vector3.up);
		}
    }
}
