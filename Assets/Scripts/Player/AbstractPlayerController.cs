using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class AbstractPlayerController : NetworkBehaviour
    {
        private CharacterController _characterController;
        private Animator _animator;
        private InputActions _inputActions;

        [SerializeField]
        private float walkSpeed = 3;

        [SerializeField]
        private float runSpeed = 3;

        [SerializeField]
        private float rotationSpeed = 3;

        protected virtual void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            var networkAnimator = GetComponent<NetworkAnimator>();
            if (networkAnimator != null) networkAnimator.animator = _animator;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            _inputActions = new InputActions();
            _inputActions.Player.Enable();
        }

        protected virtual void FixedUpdate()
        {
            // Player Input
            if (!isLocalPlayer) return;
            var input = _inputActions.Player.Move.ReadValue<Vector2>();
            var isRunning = _inputActions.Player.Run.inProgress;
            var speed = isRunning ? runSpeed : walkSpeed;
            
            // Character Movement
            var t = transform;
            var move = Math.Clamp(input.y, 0, 1) * speed * t.forward;
            _characterController.SimpleMove(move);

            var rotation = input.x * rotationSpeed * t.up;
            transform.Rotate(rotation);
            
            // Animator update
            if (_animator == null) return;
            _animator.SetFloat(C.ANIMATOR_PARAMETER_SPEED, _characterController.velocity.magnitude);
        }

        /*
        private void OnEnable()
        {
            if (!isLocalPlayer) return;
//            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            if (!isLocalPlayer) return;
            _inputActions.Player.Disable();
        }
        */

    }
}
