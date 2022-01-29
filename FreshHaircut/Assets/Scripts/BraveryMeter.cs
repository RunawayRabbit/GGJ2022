using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BraveryMeter : MonoBehaviour
{
    public float maxBravery = 50f;
    public float frightValue = 0f;
    public float braveryQuota = 5f;

    [SerializeField] private GameObject _camera;
    
    public float _currentBravery = 0f;
    private bool _isRunningToBed = false;
    private bool _isFrightened = false;
    private Vector3 _bedPosition;
    private NavMeshAgent _agent;
    private PlayerController _controller;
    private MusicManager _musicManager;
    
    private void Awake()
    {
        _musicManager = _camera.GetComponent<MusicManager>();
        _controller = GetComponent<PlayerController>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = false;

        _isFrightened = false;
        _bedPosition = GameObject.FindWithTag("Bed").transform.position;
    }

    private void Start()
    {
        ResetBravery();
        ResetFrightValue();
    }

    private void Update()
    {
        _currentBravery = Mathf.Clamp(_currentBravery - frightValue * Time.deltaTime, 0, maxBravery);

        if (!_isFrightened && _currentBravery < braveryQuota)
        {
            Debug.Log("hello i am in here");
            _isFrightened = true;
            _musicManager.PlayStem(musicStates.frightened);
        }

        if (!_isRunningToBed && _currentBravery <= 0)
        {
            _isRunningToBed = true;
            _agent.enabled = true;
            _agent.SetDestination(_bedPosition);
            _controller.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            ResetBravery();
            ResetFrightValue();
            _agent.enabled = false;
            _controller.enabled = true;
            _isRunningToBed = false;
        }
        
        else if (other.gameObject.CompareTag("DarkArea"))
        {
            ModifyDecreaseValue(1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DarkArea"))
        {
            ResetFrightValue();
        }
        
        else if (other.gameObject.CompareTag("Bed"))
        {
            ResetBravery();
            ResetFrightValue();
            _agent.enabled = false;
            _controller.enabled = true;
            _isRunningToBed = false;
        }
    }

    public void ModifyDecreaseValue(float newValue)
    {
        frightValue = newValue;
    }

    private void ResetFrightValue()
    {
        frightValue = baseFrightValue;
    }

    private void ResetBravery()
    {
        _currentBravery = maxBravery;

        if (_isFrightened == true)
        {
            _isFrightened = false;
            _musicManager.PlayStem(musicStates.main);
        }
    }
}
