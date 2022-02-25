using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheBitCave.MultiplayerRoguelite
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        private CharacterController _characterController;
        private InputActions _inputActions;

        [SerializeField]
        private float playerSpeed = 3;
 
        [SerializeField]
        private float rotationSpeed = 3;

        private void Awake()
        {
            _inputActions = new InputActions();
            _characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;
            
            var input = _inputActions.Player.Move.ReadValue<Vector2>();
            var move = input.y * playerSpeed * transform.forward;
            _characterController.Move(move);

            var rotation = input.x * rotationSpeed * transform.up;
            transform.Rotate(rotation);
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }

        private void OnDestroy()
        {
            _inputActions.Player.Disable();
        }
    }
    
}
