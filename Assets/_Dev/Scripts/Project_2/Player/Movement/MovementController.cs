using System;
using DG.Tweening;
using Project2.General;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	#region Variable
	public bool IsMovementOpen { get; private set; } = false;

	[SerializeField] protected Transform _forwardMovementObject;
	[SerializeField] protected Transform _horizontalMovementObject;
	[SerializeField] private Transform _chibi;
	[SerializeField] private MovementData _movementData;
	[SerializeField] private PlatformParentController _platformParentController;
	private InputManager.InputListener _inputListener;

	#endregion

	#region MonoBehaviour

	private void Start()
	{

		_inputListener = new InputManager.InputListener();
		InputManager.Instance.AddListener(_inputListener);
		LevelManager.OnLevelStarted += ResetModel;
		GameManager.OnMovement += MovementOpening;
		//GameManager.OnPlayerTouch += MoveHorizontal;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelStarted -= ResetModel;
		GameManager.OnMovement -= MovementOpening;
		//GameManager.OnPlayerTouch -= MoveHorizontal;
	}

	private void Update()
	{

		if (!IsMovementOpen)
			return;
		MoveForward(_forwardMovementObject);
	}

	#endregion

	#region Func
	private void MovementOpening(bool isOpen)
	{
		IsMovementOpen = isOpen;
	}

	#endregion

	#region Movement

	public void MoveForward(Transform _forwardMovementObject)
	{
		float currentSpeed = _movementData.MovementSpeed;
		_forwardMovementObject.transform.position += Vector3.forward * (currentSpeed * Time.deltaTime);
	}

	public void MoveHorizontal()
	{
		Transform currentPlatform = _platformParentController.CurrentPlatform.transform;
		_horizontalMovementObject.DOLocalMoveX(currentPlatform.transform.position.x, CalculateSpeed(currentPlatform));
	}

	private float CalculateSpeed(Transform currentPlatform)
	{

		var distance = currentPlatform.transform.position.z - transform.position.z - currentPlatform.localScale.z / 2f;
		return distance / _movementData.MovementSpeed;
	}

	public void ResetHorizontal()
	{
		_horizontalMovementObject.DOLocalMoveX(0, 1f);
	}

	private void ResetModel()
	{
		Transform modelTransform = _chibi.transform;
		modelTransform.localRotation = Quaternion.Euler(0, 0, 0);
		modelTransform.localPosition = Vector3.zero;

		_forwardMovementObject.position = new Vector3(0, _forwardMovementObject.position.y, _forwardMovementObject.position.z);
	}

	#endregion
}



