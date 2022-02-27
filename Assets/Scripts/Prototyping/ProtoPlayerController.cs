using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    [RequireComponent(typeof(CharacterController))]
    public class ProtoPlayerController : AbstractPlayerController
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

        [SyncVar(hook = nameof(OnSkinColorChange))]
        private Color _skinColor;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            var networkAnimator = GetComponent<NetworkAnimator>();
            if (networkAnimator != null) networkAnimator.animator = _animator;
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            _inputActions = new InputActions();
            _inputActions.Player.Enable();
        }

        public override void OnStartServer()
        {
            _skinColor = Random.ColorHSV();
        }

        private void OnSkinColorChange(Color _, Color newColor)
        {
            var smr = GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr == null) return;
            smr.material.color = newColor;
        }

        private void FixedUpdate()
        {
            // Player Input
            if (!isLocalPlayer) return;
            var input = _inputActions.Player.Move.ReadValue<Vector2>();
            var isRunning = _inputActions.Player.Run.inProgress;
            var speed = isRunning ? runSpeed : walkSpeed;
            
            // Character Movement
            var move = input.y * speed * transform.forward;
            _characterController.Move(move);

            var rotation = input.x * rotationSpeed * transform.up;
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
