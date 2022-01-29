using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	private GameObject _player;
	private void Awake()
	{
		_player = GameObject.FindWithTag( "Player" );
	}

	void Update()
    {
       transform.LookAt(_player.transform.position + Vector3.up, Vector3.up);
    }
}
