using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	public float movementSpeed = 20f;

	private bool isMoving;
	private Vector2 _currentInputs;
	private Vector3 _currentVelocity;
	private MouseInputController InputController = null;
	private PlayerSFXManager _sfxManager;

	private Camera CurrentCamera = null;

	private void Awake()
	{
		InputController = new MouseInputController();
		_sfxManager = GetComponent<PlayerSFXManager>();
		CurrentCamera = Camera.main;
	}

	private void Update()
	{
		DoTheMove();
	}

	private void OnEnable()
	{
		InputController.PlayerOne.Move.performed += OnSetMovementInput;
		InputController.PlayerOne.Move.canceled += OnSetMovementInput;

		InputController.Enable();

		_currentVelocity = Vector3.zero;
		_currentInputs   = Vector2.zero;
	}

	private void OnDisable()
	{
		InputController.PlayerOne.Move.performed -= OnSetMovementInput;
		InputController.PlayerOne.Move.canceled -= OnSetMovementInput;

		InputController.Disable();

		_currentVelocity = Vector3.zero;
		_currentInputs   = Vector2.zero;
	}

	private void OnSetMovementInput(InputAction.CallbackContext obj)
	{
		_currentInputs = obj.ReadValue<Vector2>();
	}

	private void DoTheMove()
	{
		Vector3 yAxis = Vector3.ProjectOnPlane( CurrentCamera.transform.forward, Vector3.up );
		Vector3 xAxis = Vector3.Cross( Vector3.up, yAxis );

		Vector3 deltaMove = Vector3.zero;
		deltaMove += yAxis * _currentInputs.y * movementSpeed * Time.deltaTime;
		deltaMove += xAxis * _currentInputs.x * movementSpeed * Time.deltaTime;

		transform.position += deltaMove;

		if (!isMoving && deltaMove != Vector3.zero)
		{
			isMoving = true;
			_sfxManager.Walk(isMoving);
		}
		
		else if (isMoving && deltaMove == Vector3.zero)
		{
			isMoving = false;
			_sfxManager.Walk(isMoving);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(_currentVelocity.x, _currentVelocity.y));
	}
}
