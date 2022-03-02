using System;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Prototype;
using UnityEngine;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine.Serialization;

namespace TheBitCave.MultiplayerRoguelite
{
    public abstract class AbstractCharacter : NetworkBehaviour
    {
        [Header("Stats")]
        [SerializeField]
        private CharacterStatsSO stats;

        [Header("Components")]
        [SerializeField]
        private Animator animator;
        
        private InputActions _inputActions;

        protected virtual void Awake()
        {
            _inputActions = InputManager.Instance.Actions;
            var networkAnimator = GetComponent<NetworkAnimator>();
            if (networkAnimator != null) networkAnimator.animator = animator;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            _inputActions.Player.Enable();
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

        #region Getters and Setters

        public Animator Animator => animator;
        public CharacterStatsSO Stats => stats;
        public InputActions InputActions => _inputActions;

        #endregion
    }
}
