using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	public float accelerationValue = 10f;
	public float maxMovementSpeed = 20f;

	private Rigidbody _rigidbody;
	private Vector2 _currentInputs;
	private Vector3 _currentVelocity;
	private MouseInputController InputController = null;

	private Camera CurrentCamera = null;

	private void Awake()
	{
		InputController = new MouseInputController();
		CurrentCamera = Camera.main;
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		SolveVelocity();
		DoTheMove();
	}

	private void OnEnable()
	{
		// InputController.PlayerOne.ConfirmMovementPoint.performed += OnSetWaypoint;
		InputController.PlayerOne.Move.performed += OnSetMovementInput;
		InputController.PlayerOne.Move.canceled += OnSetMovementInput;
		
		InputController.Enable();
	}

	private void OnDisable()
	{
		//InputController.PlayerOne.ConfirmMovementPoint.performed -= OnSetWaypoint;
		InputController.PlayerOne.Move.performed -= OnSetMovementInput;
		InputController.PlayerOne.Move.canceled -= OnSetMovementInput;
		
		InputController.Disable();
	}
	
	private void OnSetWaypoint(InputAction.CallbackContext obj)
	{
		// Vector3 worldMousePosition = CurrentCamera.ScreenToWorldPoint()
	}

	private void OnSetMovementInput(InputAction.CallbackContext obj)
	{
		_currentInputs = obj.ReadValue<Vector2>();
	}

	private void DoTheMove()
	{
		transform.position += _currentVelocity * Time.deltaTime;
	
		//_rigidbody.AddForce(_currentVelocity);
		//_rigidbody.velocity = _currentVelocity;
	}

	private void SolveVelocity()
	{

		Vector3 yAxis = Vector3.ProjectOnPlane( CurrentCamera.transform.forward, Vector3.up );
		Vector3 xAxis = Vector3.Cross( Vector3.up, yAxis );
		
		Debug.DrawLine(transform.position, transform.position + xAxis);
		
		_currentVelocity += yAxis * _currentInputs.y * accelerationValue * Time.deltaTime;
		_currentVelocity += xAxis * _currentInputs.x * accelerationValue * Time.deltaTime;
		
		_currentVelocity.x = Mathf.Clamp(_currentVelocity.x, -maxMovementSpeed, maxMovementSpeed);
		_currentVelocity.y = Mathf.Clamp(_currentVelocity.y, -maxMovementSpeed, maxMovementSpeed);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(_currentVelocity.x, _currentVelocity.y));
	}
}
