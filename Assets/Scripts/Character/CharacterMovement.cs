using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    /// <summary>
    /// Character movement ability. Handles walking, running and rotating
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AbstractCharacter))]
    public class CharacterMovement : NetworkBehaviour
    {
        private CharacterController _characterController;
        private AbstractCharacter _character;
     
        /// <summary>
        /// Initializes all needed component references.
        /// </summary>
        protected virtual void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _character = GetComponent<AbstractCharacter>();
        }

        /// <summary>
        /// Handles local player input
        /// </summary>
        [ClientCallback]
        protected virtual void FixedUpdate()
        {
            // Check input only if the character is a local player
            if (!isLocalPlayer) return;

            // Player Input
            var input = _character.InputActions.Player.Move.ReadValue<Vector2>();
            var isRunning = _character.InputActions.Player.Run.inProgress;
            var speed = isRunning ? _character.Stats.RunSpeed : _character.Stats.WalkSpeed;
            
            // Character Movement
            var t = transform;
            var move = Mathf.Clamp01(input.y) * speed * t.forward;
            _characterController.SimpleMove(move);

            var rotation = input.x * _character.Stats.RotationSpeed * t.up;
            transform.Rotate(rotation);
            
            // Animator update
            if (_character.Animator == null) return;
            _character.Animator.SetFloat(C.ANIMATOR_PARAMETER_SPEED, _characterController.velocity.magnitude);
        }
    }
    
}
