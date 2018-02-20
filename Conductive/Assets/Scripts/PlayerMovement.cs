using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Movement variables
    [SerializeField] Transform _rotationTransform;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _gravity;

    // Character references
    CharacterController _characterController;

    // Rotation variables and references
    [SerializeField] float _rotationSpeed;
    Quaternion _targetRotation;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ApplyGravity();

        Vector3 moveDirection = GetMoveInput();
        HandleMovement(moveDirection);
        HandleRotation(moveDirection);
    }

    Vector3 GetMoveInput()
    {
        if (_characterController.isGrounded)
            return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        return Vector3.zero;
    }

    void ApplyGravity()
    {
        _characterController.Move(Vector3.up * -_gravity * Time.deltaTime);
    }

    void HandleMovement(Vector3 inMoveDirection)
    {
        _characterController.Move(inMoveDirection * Time.deltaTime * _movementSpeed);
    }

    void HandleRotation(Vector3 inMoveDirection)
    {
        if (inMoveDirection == Vector3.zero)
            return;

        Quaternion currentRotation = _rotationTransform.rotation;
        _targetRotation = Quaternion.LookRotation(inMoveDirection.normalized, Vector3.up);
        _rotationTransform.rotation = Quaternion.RotateTowards(currentRotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }

}
