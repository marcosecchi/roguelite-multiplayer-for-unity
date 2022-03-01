using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Prototype;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class AbstractPlayerController : NetworkBehaviour
    {
        private CharacterController _characterController;
        private Animator _animator;
        private InputActions _inputActions;

        [Header("Stats")]
        [SerializeField]
        private PlayerStatsSO stats;
        
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
            var speed = isRunning ? stats.RunSpeed : stats.WalkSpeed;
            
            // Character Movement
            var t = transform;
            var move = Math.Clamp(input.y, 0, 1) * speed * t.forward;
            _characterController.SimpleMove(move);

            var rotation = input.x * stats.RotationSpeed * t.up;
            transform.Rotate(rotation);
            
            // Animator update
            if (_animator == null) return;
            _animator.SetFloat(C.ANIMATOR_PARAMETER_SPEED, _characterController.velocity.magnitude);
        }

        
        private void OnEnable()
        {
            if (!isLocalPlayer) return;
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            if (!isLocalPlayer) return;
            _inputActions.Player.Disable();
        }
    }
}
