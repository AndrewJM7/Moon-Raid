using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Set variables for player input
    public PlayerInput.OnFootActions onFoot;
    private PlayerInput playerInput;

    private PlayerMovement movement;
    private PlayerLook look;
    private PlayerShoot shoot;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        playerInput = new PlayerInput();   
        onFoot = playerInput.OnFoot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        shoot = GetComponent<PlayerShoot>();

        // Connect input to functions
        onFoot.Crouch.performed += ctx => movement.Crouch();
        onFoot.Sprint.performed += ctx => movement.Sprint();
        onFoot.Shoot.performed += ctx => shoot.Shoot();
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        // Tells the movement script to move based on action
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    // LateUpdate is called every frame, if the behaviour is enabled
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    // Enables actions
    private void OnEnable()
    {
        onFoot.Enable(); ;
    }

    // Disables actions
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
