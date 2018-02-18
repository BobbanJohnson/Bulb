using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Movement variables
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float antiBumpFactor;
    public int movementHorizontal, movementVertical = 0;

    // Character references
    private Transform playerGraphics;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;

    // Rotation variables and references
    [SerializeField] private float rotationSpeed;
    private Transform target;
    private Quaternion targetRotation;
    private float rotationTime;
    private bool rotating = false;


    void Start()
    {
        playerGraphics = transform.GetChild(0);
        target = transform.GetChild(1);
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Create a movement vector to move charactercontroller
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), -antiBumpFactor, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        // Check if the player is moving up or down;
        if (Input.GetKeyDown(KeyCode.S))
            movementVertical = -5;

        if (Input.GetKeyUp(KeyCode.S))
            movementVertical = 0;

        if (Input.GetKeyDown(KeyCode.W))
            movementVertical = 5;

        if (Input.GetKeyUp(KeyCode.W))
            movementVertical = 0;

         //Check if the player is moving up or down;
        if (Input.GetKeyDown(KeyCode.A))
            movementHorizontal = -5;

        if (Input.GetKeyUp(KeyCode.A))
            movementHorizontal = 0;

        if (Input.GetKeyDown(KeyCode.D))
            movementHorizontal = 5;

        if (Input.GetKeyUp(KeyCode.D))
            movementHorizontal = 0;

        MovePositionToLookAt(new Vector3(movementHorizontal, 0, movementVertical));

        /*
        // Move rotation transform depending on input
        /// Vertical movement rotation
        if (Input.GetKeyDown(KeyCode.S))
            MovePositionToLookAt(new Vector3(0, 0, -5));

        else if (Input.GetKeyDown(KeyCode.W))
            MovePositionToLookAt(new Vector3(0, 0, 5));

        /// Horizontal movement rotation
        if (Input.GetKeyDown(KeyCode.A))
            MovePositionToLookAt(new Vector3(-5, 0, 0));

        else if (Input.GetKeyDown(KeyCode.D))
            MovePositionToLookAt(new Vector3(5, 0, 0));
        



        /// Upper diagnoal movement rotation
        if (Input.GetKeyDown(KeyCode.W) && (Input.GetKeyDown(KeyCode.A)))
            MovePositionToLookAt(new Vector3(-5, 0, 5));

        else if (Input.GetKeyDown(KeyCode.W) && (Input.GetKeyDown(KeyCode.D)))
            MovePositionToLookAt(new Vector3(5, 0, 5));

        /// Lower diagnoal movement rotation
        if (Input.GetKeyDown(KeyCode.S) && (Input.GetKeyDown(KeyCode.A)))
            MovePositionToLookAt(new Vector3(-5, 0, -5));

        else if (Input.GetKeyDown(KeyCode.S) && (Input.GetKeyDown(KeyCode.D)))
            MovePositionToLookAt(new Vector3(5, 0, -5));
            */


        // Rotate the player towards a moving transform
        if (rotating)
        {
            rotationTime += Time.deltaTime * rotationSpeed;
            playerGraphics.rotation = Quaternion.Lerp(playerGraphics.rotation, targetRotation, rotationTime);
            if (rotationTime > 1)
            {
                rotating = false;
            }
        }
    }
    
    // Rotation to move the character
    void MovePositionToLookAt(Vector3 targetPosition)
    {
        target.position = transform.position + targetPosition;
        Vector3 relativePosition = target.position - playerGraphics.position;
        targetRotation = Quaternion.LookRotation(relativePosition);
        rotating = true;
        rotationTime = 0;
    }
}
