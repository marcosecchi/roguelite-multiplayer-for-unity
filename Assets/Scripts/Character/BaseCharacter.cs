using System;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using TheBitCave.MultiplayerRoguelite.Prototype;
using UnityEngine;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine.Serialization;

namespace TheBitCave.MultiplayerRoguelite
{
    [RequireComponent(typeof(CharacterController))]
    public partial class BaseCharacter : NetworkBehaviour
    {
        [Header("Stats")]
        [SerializeField]
        private CharacterStatsSO stats;

        [Header("Components")]
        [SerializeField]
        private Animator animator;
        
        private InputActions _inputActions;
        private CharacterController _characterController;
        private AbilityAttack _abilityAttack;
        private NetworkAnimator _networkAnimator;

        protected virtual void Awake()
        {
            _inputActions = InputManager.Instance.Actions;
            _characterController = GetComponent<CharacterController>();
            _abilityAttack = GetComponent<AbilityAttack>();
            _networkAnimator = GetComponent<NetworkAnimator>();
            if (_networkAnimator != null) _networkAnimator.animator = animator;
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
        public NetworkAnimator NetworkAnimator => _networkAnimator;
        public CharacterStatsSO Stats => stats;

        #endregion
    }
}
