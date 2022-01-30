using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerSFXManager))]
public class BraveryMeter : MonoBehaviour
{
    public float maxBravery = 50f;
    public float baseFrightValue = 1.0f;
    public float frightValue = 0f;
    public float braveryQuota = 5f;
    public float _currentBravery = 0f;

    private int addValue;
    private bool _isRunningToBed = false;
    private bool _isFrightened = false;
    private Vector3 _bedPosition;
    private NavMeshAgent _agent;
    private PlayerController _controller;
    private MusicManager _musicManager;
    private PlayerSFXManager _sfxManager;
    private Animator _animationController;

    public float GetCurrentBravery() => _currentBravery;

    private void Awake()
    {
        _musicManager  = GameObject.FindObjectOfType<MusicManager>();
        _controller    = GetComponent<PlayerController>();
        _sfxManager    = GetComponent<PlayerSFXManager>();
        _agent         = GetComponent<NavMeshAgent>();
        _agent.enabled = false;
        _animationController = GetComponent<Animator>();

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
        _animationController.SetFloat("bravery", _currentBravery/maxBravery);

        if (!_isFrightened && _currentBravery < braveryQuota)
        {
            _isFrightened = true;
            _musicManager.PlayStem(musicStates.frightened);
        }

        if (!_isRunningToBed && _currentBravery <= 0)
        {
            _isRunningToBed = true;
            _animationController.SetBool("outOfBravery", true);
            _agent.enabled = true;
            _agent.SetDestination(_bedPosition);
            _controller.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            if (_isFrightened)
            {
                _sfxManager.Exhale();
            }

            ResetBravery();
            ResetFrightValue();
            _animationController.SetBool("outOfBravery", false);
            _agent.enabled = false;
            _controller.enabled = true;
            _isRunningToBed = false;
        }

        else if (other.gameObject.CompareTag("DarkArea"))
        {
            addValue += other.GetComponent<DarkArea>().FrightAddValue;
            ModifyFrightValue(addValue);
        }
        else if (other.gameObject.CompareTag("LightArea"))
        {
            ResetBravery();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DarkArea"))
        {
            addValue -= other.GetComponent<DarkArea>().FrightAddValue;
            ModifyFrightValue(addValue);
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

    private void ModifyFrightValue(float newValue)
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
