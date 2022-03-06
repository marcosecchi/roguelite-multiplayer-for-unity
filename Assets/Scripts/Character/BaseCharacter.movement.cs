using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public partial class BaseCharacter : NetworkBehaviour
    {
        /// <summary>
        /// Handles local player input
        /// </summary>
        [ClientCallback]
        protected virtual void FixedUpdate()
        {
            // Check input only if the character is a local player
            if (!isLocalPlayer) return;

            // Player Input
            var input = inputActions.Player.Move.ReadValue<Vector2>();
            var isRunning = inputActions.Player.Run.inProgress;
            var speed = isRunning ? Stats.RunSpeed : Stats.WalkSpeed;
            
            // Character Movement
            var t = transform;
            var move = Mathf.Clamp01(input.y) * speed * t.forward;
            characterController.SimpleMove(move);

            var rotation = input.x * Stats.RotationSpeed * t.up;
            transform.Rotate(rotation);
            
            // Animator update
            if (Animator == null) return;
            Animator.SetFloat(C.ANIMATOR_PARAMETER_SPEED, characterController.velocity.magnitude);
            
        }
    }
    
}
