using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private MouseInputController InputController = null;

	private Camera CurrentCamera = null;

	private void Awake()
	{
		InputController = new MouseInputController();
		CurrentCamera = Camera.main;
	}

	private void OnEnable()
	{
		InputController.PlayerOne.ConfirmMovementPoint.performed += OnSetWaypoint;
		
		InputController.Enable();
	}

	private void OnDisable()
	{
		InputController.PlayerOne.ConfirmMovementPoint.performed -= OnSetWaypoint;
		
		InputController.Disable();
	}
	
	private void OnSetWaypoint(InputAction.CallbackContext obj)
	{
		// Vector3 worldMousePosition = CurrentCamera.ScreenToWorldPoint()
	}
}
